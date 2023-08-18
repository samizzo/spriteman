using spriteman.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace spriteman
{
    public partial class Form1 : Form, IMessageFilter
    {
        private const int WM_MOUSEWHEEL = 0x020a;

        private SpriteProject spriteProject = new SpriteProject();
        private Image currentImage;
        private float currentScale = 1.0f;
        private Point lastMousePosition;
        private Point mouseDownPosition;
        private Point selectionStartPosition;
        private Point selectionCurrentPosition;
        private bool mouseDownDragging;
        private bool spaceDown;
        private bool selectingSprite;
        private Point imageOrigin = new Point(0, 0);

        // P/Invoke declarations
        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point pt);
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        public Form1()
        {
            InitializeComponent();
            imagePanel.MouseWheel += imagePanel_MouseWheel;
            imageOrigin = new Point((int)(imagePanel.Size.Width * 0.5f), (int)(imagePanel.Size.Height * 0.5f));
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, imagePanel, new object[] { true });
        }

        private Rectangle GetSelectionRectangle()
        {
            int x = selectionStartPosition.X;
            int y = selectionStartPosition.Y;
            int w = selectionCurrentPosition.X - selectionStartPosition.X + 1;
            int h = selectionCurrentPosition.Y - selectionStartPosition.Y + 1;
            if (w < 0)
            {
                w *= -1;
                w += 1;
                x = selectionCurrentPosition.X;
            }
            if (h < 0)
            {
                h *= -1;
                h += 1;
                y = selectionCurrentPosition.Y;
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
            x = Math.Max(0, Math.Min(currentImage.Width, x));
            y = Math.Max(0, Math.Min(currentImage.Height, y));
            return new Point(x, y);
        }

        private void ResetScale()
        {
            currentScale = 1.0f;
            imageOrigin = new Point((int)(imagePanel.Size.Width * 0.5f), (int)(imagePanel.Size.Height * 0.5f));
            imagePanel.Refresh();
        }

        private void addImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*";
            ofd.FilterIndex = 0;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var file = ofd.FileName;
                spriteProject.Images.Add(file);
                RefreshImageList();
                imagesListBox.SelectedIndex = spriteProject.Images.Count - 1;
            }
        }

        private void RefreshImageList()
        {
            imagesListBox.Items.Clear();
            foreach (var file in spriteProject.Images)
            {
                imagesListBox.Items.Add(file);
            }
        }

        private void imagesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentImage = Image.FromFile(spriteProject.Images[imagesListBox.SelectedIndex]);
            currentScale = 1.0f;
            imagePanel.Refresh();
            imagePanel.Focus();
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
            }
        }

        private void imagePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentImage == null)
                return;

            var position = GetPixelPosition(e.Location);

            if (mouseDownDragging && spaceDown)
            {
                int deltax = e.Location.X - lastMousePosition.X;
                int deltay = e.Location.Y - lastMousePosition.Y;
                lastMousePosition = e.Location;
                imageOrigin.X += (int)(deltax);
                imageOrigin.Y += (int)(deltay);
                imagePanel.Refresh();
            }
            else if (selectingSprite)
            {
                selectionCurrentPosition = position;
                imagePanel.Refresh();
            }

            var text = new StringBuilder();
            if (selectingSprite)
            {
                var rect = GetSelectionRectangle();
                text.Append($"W:{rect.Width} H:{rect.Height} ");
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
            mouseDownPosition = lastMousePosition = e.Location;
            selectionStartPosition = GetPixelPosition(e.Location);
            selectingSprite = Control.ModifierKeys == Keys.Shift;
        }

        private void imagePanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (currentImage == null)
                return;

            mouseDownDragging = false;
            spaceDown = false;
            if (selectingSprite)
            {
                selectingSprite = false;
                Refresh();
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
    }
}
