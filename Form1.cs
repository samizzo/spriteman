﻿using spriteman.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace spriteman
{
    public partial class MainForm : Form, IMessageFilter
    {
        private const int WM_MOUSEWHEEL = 0x020a;

        enum EdgeDrag
        {
            Left,
            Right,
            Top,
            Bottom,
            None
        }

        private SpriteProject spriteProject = new SpriteProject();
        private Image currentImage;
        private float currentScale = 1.0f;
        private Point lastMousePosition;
        private Point selectionStartPosition;
        private Point selectionCurrentPosition;
        private bool mouseDownDragging;
        private bool spaceDown;
        private bool selectingSprite;
        private Point imageOrigin = new Point(0, 0);
        private BindingList<Sprite> sprites;
        private BindingList<string> images;
        private Sprite currentSprite;
        private EdgeDrag currentSpriteEdgeDrag = EdgeDrag.None;
        private RectangleF currentSpriteRect;

        // P/Invoke declarations
        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point pt);
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        public MainForm()
        {
            InitializeComponent();
            imagePanel.MouseWheel += imagePanel_MouseWheel;
            imageOrigin = new Point((int)(imagePanel.Size.Width * 0.5f), (int)(imagePanel.Size.Height * 0.5f));
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, imagePanel, new object[] { true });

            imagesToolStrip.Renderer = new ToolStripSystemRendererEx();
            sprites = new BindingList<Sprite>(spriteProject.Sprites);
            spritesListBox.DataSource = sprites;
            spritesListBox.DisplayMember = "Name";

            images = new BindingList<string>(spriteProject.Images);
            imagesListBox.DataSource = images;
        }

        private Rectangle GetSelectionRectangle()
        {
            int x, y, w, h;
            if (selectionCurrentPosition.X < selectionStartPosition.X)
            {
                x = selectionCurrentPosition.X;
                w = selectionStartPosition.X - x + 1;
            }
            else
            {
                x = selectionStartPosition.X;
                w = selectionCurrentPosition.X - x + 1;
            }

            if (selectionCurrentPosition.Y < selectionStartPosition.Y)
            {
                y = selectionCurrentPosition.Y;
                h = selectionStartPosition.Y - y + 1;
            }
            else
            {
                y = selectionStartPosition.Y;
                h = selectionCurrentPosition.Y - y + 1;
            }

            var size = new Size(w, h);
            var rect = new Rectangle(new Point(x, y), size);
            return rect;
        }

        // Return a point in pixel coordinates given a point in panel coordinates.
        private Point GetPixelPosition(Point point)
        {
            int x = (int)((point.X / currentScale) - ((imageOrigin.X / currentScale) - (currentImage.Width * 0.5f)));
            int y = (int)((point.Y / currentScale) - ((imageOrigin.Y / currentScale) - (currentImage.Height * 0.5f)));
            return new Point(x, y);
        }

        // Return a point in floating point pixel coordinates given a point in panel coordinates.
        private PointF GetPixelPositionF(Point point)
        {
            var x = (point.X / currentScale) - ((imageOrigin.X / currentScale) - (currentImage.Width * 0.5f));
            var y = (point.Y / currentScale) - ((imageOrigin.Y / currentScale) - (currentImage.Height * 0.5f));
            return new PointF(x, y);
        }

        // Return a drag handle that the given mouse point is over.
        private EdgeDrag GetSpriteEdgeDrag(Point point)
        {
            Debug.Assert(currentSprite != null);
            float tlx = currentSprite.X;
            float tly = currentSprite.Y;
            float brx = tlx + currentSprite.Width;
            float bry = tly + currentSprite.Height;
            var posf = GetPixelPositionF(point);
            if (posf.X < 0 || posf.Y < 0 || posf.X >= currentImage.Width || posf.Y >= currentImage.Height)
            {
                return EdgeDrag.None;
            }
            else if (posf.X >= (tlx - 0.5f) && posf.X <= (tlx + 0.5f) && posf.Y >= tly && posf.Y < bry)
            {
                return EdgeDrag.Left;
            }
            else if (posf.X >= (brx - 0.5f) && posf.X <= (brx + 0.5f) && posf.Y >= tly && posf.Y < bry)
            {
                return EdgeDrag.Right;
            }
            else if (posf.Y >= (bry - 0.5f) && posf.Y <= (bry + 0.5f) && posf.X >= tlx && posf.X < brx)
            {
                return EdgeDrag.Bottom;
            }
            else if (posf.Y >= (tly - 0.5f) && posf.Y <= (tly + 0.5f) && posf.X >= tlx && posf.X < brx)
            {
                return EdgeDrag.Top;
            }
            else
            {
                return EdgeDrag.None;
            }
        }

        private void SetMouseCursor(Point point)
        {
            var dragHandle = GetSpriteEdgeDrag(point);
            switch (dragHandle)
            {
                case EdgeDrag.Left:
                case EdgeDrag.Right:
                    Cursor = Cursors.SizeWE;
                    break;
                case EdgeDrag.Top:
                case EdgeDrag.Bottom:
                    Cursor = Cursors.SizeNS;
                    break;
                default:
                    Cursor = Cursors.Default;
                    break;
            }
        }

        private void ResetScale()
        {
            currentScale = 1.0f;
            imageOrigin = new Point((int)(imagePanel.Size.Width * 0.5f), (int)(imagePanel.Size.Height * 0.5f));
            imagePanel.Refresh();
        }

        private void AddSprite()
        {
            var spriteNameForm = new SpriteNameForm();
            if (spriteNameForm.ShowDialog() == DialogResult.OK)
            {
                var currentImage = imagesListBox.SelectedItem;
                var rect = GetSelectionRectangle();
                var image = currentImage.ToString();
                Debug.Assert(spriteProject.Images.Contains(image));
                var sprite = new Sprite()
                {
                    Image = image,
                    Name = spriteNameForm.SpriteName,
                    X = rect.X,
                    Y = rect.Y,
                    Width = rect.Width,
                    Height = rect.Height
                };
                sprites.Add(sprite);
                // Clear the selected item and re-set it so the selection changed handler is called.
                spritesListBox.SelectedIndex = -1;
                spritesListBox.SelectedItem = sprite;
                imagePanel.Refresh();
            }
        }

        private void imagesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var image = imagesListBox.SelectedIndex >= 0 ? spriteProject.Images[imagesListBox.SelectedIndex] : "";
            if (!string.IsNullOrEmpty(image))
            {
                currentImage = Image.FromFile(image);
            }
            else
            {
                currentImage = null;
            }

            var sprite = spritesListBox.SelectedItem as Sprite;
            if (sprite == null || sprite.Image != image)
                spritesListBox.SelectedIndex = -1;
            currentScale = 1.0f;
            imagePanel.Refresh();
        }

        private void imagePanel_MouseWheel(object sender, MouseEventArgs e)
        {
            //imageOrigin.X -= (int)((e.Location.X - imageOrigin.X) / currentScale);
            //imageOrigin.Y -= (int)((e.Location.Y - imageOrigin.Y) / currentScale);
            currentScale *= (float)Math.Exp(e.Delta / 1000.0f);
            currentScale = Math.Max(0.1f, currentScale);
            imagePanel.Refresh();
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_MOUSEWHEEL)
            {
                // Find the control at screen position m.LParam
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                IntPtr hWnd = WindowFromPoint(pos);
                if (hWnd != IntPtr.Zero && hWnd != m.HWnd && Control.FromHandle(hWnd) != null)
                {
                    SendMessage(hWnd, m.Msg, m.WParam, m.LParam);
                    return true;
                }
            }
            return false;
        }

        private void imagePanel_Paint(object sender, PaintEventArgs e)
        {
            if (currentImage != null)
            {
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

                var rect = new Rectangle(0, 0, currentImage.Width, currentImage.Height);
                var bgRect = rect;
                bgRect.Width = (int)(bgRect.Width * currentScale);
                bgRect.Height = (int)(bgRect.Height * currentScale);

                e.Graphics.ResetTransform();
                e.Graphics.TranslateTransform(imageOrigin.X - (bgRect.Width * 0.5f), imageOrigin.Y - (bgRect.Height * 0.5f), System.Drawing.Drawing2D.MatrixOrder.Append);
                using (var brush = new TextureBrush(Resources.panelbg, System.Drawing.Drawing2D.WrapMode.Tile))
                {
                    e.Graphics.FillRectangle(brush, bgRect);
                }

                e.Graphics.ResetTransform();
                e.Graphics.TranslateTransform(-currentImage.Width * 0.5f, -currentImage.Height * 0.5f);
                e.Graphics.ScaleTransform(currentScale, currentScale, System.Drawing.Drawing2D.MatrixOrder.Append);
                e.Graphics.TranslateTransform(imageOrigin.X, imageOrigin.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
                e.Graphics.DrawImage(currentImage, rect);

                if (selectingSprite)
                {
                    using (var pen = new Pen(Color.White, 2 / currentScale))
                    {
                        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        var selectionRect = GetSelectionRectangle();
                        e.Graphics.DrawRectangle(pen, selectionRect);
                    }
                }
                else if (currentSprite != null)
                {
                    using (var pen = new Pen(Color.White, 2 / currentScale))
                    {
                        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        var selectionRect = new Rectangle(currentSprite.X, currentSprite.Y, currentSprite.Width, currentSprite.Height);
                        e.Graphics.DrawRectangle(pen, selectionRect);
                    }
                }
            }
        }

        private void imagePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentImage == null)
                return;

            var position = GetPixelPosition(e.Location);
            position.X = Math.Max(0, Math.Min(currentImage.Width - 1, position.X));
            position.Y = Math.Max(0, Math.Min(currentImage.Height - 1, position.Y));

            int deltax = e.Location.X - lastMousePosition.X;
            int deltay = e.Location.Y - lastMousePosition.Y;
            lastMousePosition = e.Location;

            if (spaceDown)
            {
                if (mouseDownDragging)
                {
                    imageOrigin.X += (int)(deltax);
                    imageOrigin.Y += (int)(deltay);
                    imagePanel.Refresh();
                }
            }
            else if (selectingSprite)
            {
                currentSprite = null;
                selectionCurrentPosition = position;
                imagePanel.Refresh();
            }
            else if (currentSprite != null)
            {
                if (mouseDownDragging && currentSpriteEdgeDrag != EdgeDrag.None)
                {
                    var scaledDeltax = deltax / currentScale;
                    var scaledDeltay = deltay / currentScale;
                    switch (currentSpriteEdgeDrag)
                    {
                        case EdgeDrag.Left:
                            currentSpriteRect.X += scaledDeltax;
                            currentSpriteRect.Width -= scaledDeltax;
                            break;
                        case EdgeDrag.Right:
                            currentSpriteRect.Width += scaledDeltax;
                            break;
                        case EdgeDrag.Top:
                            currentSpriteRect.Y += scaledDeltay;
                            currentSpriteRect.Height -= scaledDeltay;
                            break;
                        case EdgeDrag.Bottom:
                            currentSpriteRect.Height += scaledDeltay;
                            break;
                    }

                    currentSprite.X = (int)currentSpriteRect.X;
                    currentSprite.Y = (int)currentSpriteRect.Y;
                    currentSprite.Width = (int)Math.Ceiling(currentSpriteRect.Width);
                    currentSprite.Height = (int)Math.Ceiling(currentSpriteRect.Height);

                    imagePanel.Refresh();
                }
                else
                {
                    SetMouseCursor(e.Location);
                }
            }

            var text = new StringBuilder();
            if (selectingSprite)
            {
                var rect = GetSelectionRectangle();
                text.Append($"W:{rect.Width} H:{rect.Height} ");
            }
            else if (currentSprite != null)
            {
                text.Append($"W:{currentSprite.Width} H:{currentSprite.Height} ");
            }
            text.Append($"X:{position.X} Y:{position.Y}");
            coords.Text = text.ToString();
            coords.Refresh();
        }

        private void imagePanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentImage == null)
                return;

            mouseDownDragging = true;
            lastMousePosition = e.Location;
            selectionStartPosition = GetPixelPosition(e.Location);
            selectionStartPosition.X = Math.Max(0, Math.Min(currentImage.Width - 1, selectionStartPosition.X));
            selectionStartPosition.Y = Math.Max(0, Math.Min(currentImage.Height - 1, selectionStartPosition.Y));
            selectingSprite = Control.ModifierKeys == Keys.Shift;
            if (currentSprite != null)
            {
                currentSpriteEdgeDrag = GetSpriteEdgeDrag(e.Location);
                if (currentSpriteEdgeDrag != EdgeDrag.None)
                    currentSpriteRect = new RectangleF(currentSprite.X, currentSprite.Y, currentSprite.Width, currentSprite.Height);
            }
            else
            {
                currentSpriteEdgeDrag = EdgeDrag.None;
            }
        }

        private void imagePanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (currentImage == null)
                return;

            mouseDownDragging = false;
            spaceDown = false;
            if (selectingSprite)
            {
                AddSprite();
                selectingSprite = false;
                imagePanel.Refresh();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                spaceDown = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.R)
            {
                ResetScale();
                e.Handled = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !mouseDownDragging)
            {
                spaceDown = false;
                e.Handled = true;
            }
        }

        private void spritesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentSprite = spritesListBox.SelectedItem as Sprite;
            if (currentSprite != null)
                imagesListBox.SelectedItem = currentSprite.Image;
            ResetScale();
        }

        private void toolStripAddImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*";
            ofd.FilterIndex = 0;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var file = ofd.FileName;
                imagesListBox.SelectedIndex = -1;
                images.Add(file);
                // Clear the selected item and re-set it so the selection changed handler is called.
                imagesListBox.SelectedIndex = -1;
                imagesListBox.SelectedItem = file;
            }
        }

        private void toolStripRemoveImageButton_Click(object sender, EventArgs e)
        {
            if (imagesListBox.SelectedIndex >= 0 && imagesListBox.SelectedIndex < spriteProject.Images.Count)
            {
                var image = imagesListBox.SelectedItem as string;
                images.RemoveAt(imagesListBox.SelectedIndex);

                // Remove sprites that reference the image.
                var removeList = sprites.Where(sprite => sprite.Image == image).ToList();
                foreach (var sprite in removeList)
                    sprites.Remove(sprite);
            }
        }
    }
}
