using spriteman.Properties;
using System;
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

        private SpriteProject currentSpriteProject = null;
        private Sprite currentSprite;
        private Image currentImage;

        private float currentScale = 1.0f;
        private Point lastMousePosition;
        private Point selectionStartPosition;
        private Point selectionCurrentPosition;
        private bool mouseDownDragging;
        private bool spaceDown;
        private bool selectingSprite;
        private Point imageOrigin = new Point(0, 0);
        private EdgeDrag currentSpriteEdgeDrag = EdgeDrag.None;
        private PointF currentSpriteTopLeft;
        private PointF currentSpriteBottomRight;

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
            propertiesToolStrip.Renderer = new ToolStripSystemRendererEx();
            spritesListBox.DisplayMember = "Name";

            SetProject(null);
        }

        private void SetProject(SpriteProject project)
        {
            currentSpriteProject = project;
            currentSprite = null;
            currentImage = null;

            if (project == null)
            {
                imagesListBox.DataSource = null;
                spritesListBox.DataSource = null;
            }
            else
            {
                imagesListBox.DataSource = currentSpriteProject.Images;
                spritesListBox.DataSource = currentSpriteProject.Sprites;
            }

            RefreshListView();
            imagePanel.Refresh();
        }

        private void RefreshControls()
        {
            toolStripRemoveImageButton.Enabled = imagesListBox.SelectedIndex != -1;
            toolStripAddImageButton.Enabled = currentSpriteProject != null;
            toolStripAddKvpButton.Enabled = currentSprite != null;
            toolStripDeleteKvpButton.Enabled = kvpListView.SelectedIndex != -1;
            imagesListBox.Enabled = currentSpriteProject != null;
            spritesListBox.Enabled = currentSpriteProject != null;

            newProjectToolStripMenuItem.Enabled = true;
            openProjectToolStripMenuItem.Enabled = true;
            saveProjectToolStripMenuItem.Enabled = currentSpriteProject != null;

            Text = "Spriteman";
            if (currentSpriteProject != null)
            {
                var filename = string.IsNullOrEmpty(currentSpriteProject.Filename) ? "<unsaved>" : currentSpriteProject.Filename;
                Text += $" - {filename}";
                if (currentSpriteProject.Dirty)
                    Text += "*";
            }
        }

        private void RefreshListView()
        {
            kvpListView.SetObjects(currentSprite?.Kvps);
            RefreshControls();
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
            float tlx = currentSprite.TopLeftX;
            float tly = currentSprite.TopLeftY;
            float brx = currentSprite.BottomRightX + 1;
            float bry = currentSprite.BottomRightY + 1;
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
                Debug.Assert(currentSpriteProject.Images.Contains(image));
                var sprite = currentSpriteProject.AddSprite(image, spriteNameForm.SpriteName, rect);
                // Clear the selected item and re-set it so the selection changed handler is called the first time.
                spritesListBox.SelectedIndex = -1;
                spritesListBox.SelectedItem = sprite;
                imagePanel.Refresh();
                RefreshControls();
            }
        }

        private void imagesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var image = imagesListBox.SelectedIndex >= 0 ? currentSpriteProject.Images[imagesListBox.SelectedIndex] : "";
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
            ResetScale();
            RefreshControls();
        }

        private void imagePanel_MouseWheel(object sender, MouseEventArgs e)
        {
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
                        var selectionRect = new Rectangle(currentSprite.TopLeftX, currentSprite.TopLeftY, currentSprite.BottomRightX - currentSprite.TopLeftX + 1, currentSprite.BottomRightY - currentSprite.TopLeftY + 1);
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
            position.X = MathHelpers.Clamp(position.X, 0, currentImage.Width - 1);
            position.Y = MathHelpers.Clamp(position.Y, 0, currentImage.Height - 1);

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
                            currentSpriteTopLeft.X += scaledDeltax;
                            break;
                        case EdgeDrag.Right:
                            currentSpriteBottomRight.X += scaledDeltax;
                            break;
                        case EdgeDrag.Top:
                            currentSpriteTopLeft.Y += scaledDeltay;
                            break;
                        case EdgeDrag.Bottom:
                            currentSpriteBottomRight.Y += scaledDeltay;
                            break;
                    }

                    currentSprite.TopLeftX = Math.Min(MathHelpers.Clamp((int)Math.Floor(currentSpriteTopLeft.X), 0, currentImage.Width - 1), currentSprite.BottomRightX);
                    currentSprite.TopLeftY = Math.Min(MathHelpers.Clamp((int)Math.Floor(currentSpriteTopLeft.Y), 0, currentImage.Height - 1), currentSprite.BottomRightY);
                    currentSprite.BottomRightX = Math.Max(MathHelpers.Clamp((int)Math.Ceiling(currentSpriteBottomRight.X), 0, currentImage.Width - 1), currentSprite.TopLeftX);
                    currentSprite.BottomRightY = Math.Max(MathHelpers.Clamp((int)Math.Ceiling(currentSpriteBottomRight.Y), 0, currentImage.Height - 1), currentSprite.TopLeftY);

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
                var width = currentSprite.BottomRightX - currentSprite.TopLeftX + 1;
                var height = currentSprite.BottomRightY - currentSprite.TopLeftY + 1;
                text.Append($"W:{width} H:{height} ");
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
            selectionStartPosition.X = MathHelpers.Clamp(selectionStartPosition.X, 0, currentImage.Width - 1);
            selectionStartPosition.Y = MathHelpers.Clamp(selectionStartPosition.Y, 0, currentImage.Height - 1);
            selectingSprite = Control.ModifierKeys == Keys.Shift;
            if (currentSprite != null)
            {
                currentSpriteEdgeDrag = GetSpriteEdgeDrag(e.Location);
                if (currentSpriteEdgeDrag != EdgeDrag.None)
                {
                    currentSpriteTopLeft = new PointF(currentSprite.TopLeftX, currentSprite.TopLeftY);
                    currentSpriteBottomRight = new PointF(currentSprite.BottomRightX, currentSprite.BottomRightY);
                }
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
            imagePanel.Refresh();
            RefreshListView();
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
                currentSpriteProject.AddImage(file);

                // Clear the selected item and re-set it so the selection changed handler is called.
                imagesListBox.SelectedIndex = -1;
                imagesListBox.SelectedItem = file;
                RefreshControls();
            }
        }

        private void toolStripRemoveImageButton_Click(object sender, EventArgs e)
        {
            if (imagesListBox.SelectedIndex >= 0 && imagesListBox.SelectedIndex < currentSpriteProject.Images.Count)
            {
                var image = imagesListBox.SelectedItem as string;
                currentSpriteProject.RemoveImage(image);
                RefreshControls();
            }
        }

        private void toolStripAddKvpButton_Click(object sender, EventArgs e)
        {
            if (currentSprite == null)
                return;
            currentSprite.Kvps.Add(new Sprite.Kvp() { Key = "key", Value = "value" });
            RefreshListView();
        }

        private void toolStripDeleteKvpButton_Click(object sender, EventArgs e)
        {
            if (currentSprite == null)
                return;
            if (kvpListView.SelectedItem != null)
            {
                currentSprite.Kvps.RemoveAt(kvpListView.SelectedIndex);
                RefreshListView();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool CheckSaveProject()
        {
            if (currentSpriteProject != null && currentSpriteProject.Dirty)
            {
                var result = MessageBox.Show("Save current project first?", "Save", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Yes:
                        var saved = SaveProject(currentSpriteProject);
                        if (saved)
                            return true;
                        break;
                    case DialogResult.No:
                        return true;
                    case DialogResult.Cancel:
                        break;
                }
            }
            else
            {
                return true;
            }

            return false;
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckSaveProject())
            {
                var ofd = new OpenFileDialog();
                ofd.Filter = "Sprite Project files (*.prj)|*.prj|All files (*.*)|*.*";
                ofd.RestoreDirectory = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                    SetProject(SpriteProject.Load(ofd.FileName));
            }
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckSaveProject())
                SetProject(new SpriteProject());
        }

        private bool SaveProject(SpriteProject spriteProject)
        {
            if (string.IsNullOrEmpty(spriteProject.Filename))
            {
                var sfd = new SaveFileDialog();
                sfd.Filter = "Sprite Project files (*.prj)|*.prj|All files (*.*)|*.*";
                sfd.RestoreDirectory = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    spriteProject.Filename = sfd.FileName;
                }
                else
                {
                    return false;
                }
            }

            spriteProject.Save();
            RefreshControls();

            return true;
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveProject(currentSpriteProject);
        }

        private void kvpListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshControls();
        }

        private void kvpListView_CellEditFinished(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            RefreshControls();
        }
    }
}
