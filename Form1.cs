using spriteman.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace spriteman
{
    public partial class Form1 : Form, IMessageFilter
    {
        private SpriteProject spriteProject = new SpriteProject();
        private Image currentImage;
        private float currentScale = 1.0f;
        private Point lastMousePosition;
        private bool mouseDownDragging;
        private bool spaceDown;
        private Point imageOrigin = new Point(0, 0);

        // P/Invoke declarations
        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point pt);
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        public Form1()
        {
            InitializeComponent();
            panel1.MouseWheel += panel1_MouseWheel;
            imageOrigin = new Point((int)(panel1.Size.Width * 0.5f), (int)(panel1.Size.Height * 0.5f));
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, panel1, new object[] { true });
        }

        private void ResetScale()
        {
            currentScale = 1.0f;
            imageOrigin = new Point((int)(panel1.Size.Width * 0.5f), (int)(panel1.Size.Height * 0.5f));
            panel1.Refresh();
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
            panel1.Refresh();
            panel1.Focus();
        }

        private void panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            //imageOrigin.X -= (int)((e.Location.X - imageOrigin.X) / currentScale);
            //imageOrigin.Y -= (int)((e.Location.Y - imageOrigin.Y) / currentScale);
            currentScale *= (float)Math.Exp(e.Delta / 1000.0f);
            currentScale = Math.Max(0.1f, currentScale);
            panel1.Refresh();
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x20a)
            {
                // WM_MOUSEWHEEL, find the control at screen position m.LParam
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

        private void panel1_Paint(object sender, PaintEventArgs e)
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
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentImage == null)
                return;

            int x = (int)((e.Location.X / currentScale) - ((imageOrigin.X / currentScale) - (currentImage.Width * 0.5f)));
            int y = (int)((e.Location.Y / currentScale) - ((imageOrigin.Y / currentScale) - (currentImage.Height * 0.5f)));
            x = Math.Max(0, Math.Min(currentImage.Width, x));
            y = Math.Max(0, Math.Min(currentImage.Height, y));
            coords.Text = $"X: {x} Y: {y}";

            if (mouseDownDragging && spaceDown)
            {
                int deltax = e.Location.X - lastMousePosition.X;
                int deltay = e.Location.Y - lastMousePosition.Y;
                lastMousePosition = e.Location;
                imageOrigin.X += (int)(deltax);
                imageOrigin.Y += (int)(deltay);
                panel1.Refresh();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownDragging = true;
            lastMousePosition = e.Location;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDownDragging = false;
            spaceDown = false;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
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
