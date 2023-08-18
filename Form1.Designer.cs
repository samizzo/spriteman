namespace spriteman
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addImageButton = new System.Windows.Forms.Button();
            this.removeImageButton = new System.Windows.Forms.Button();
            this.spritesListBox = new System.Windows.Forms.ListBox();
            this.imagePanel = new System.Windows.Forms.Panel();
            this.imagesListBox = new System.Windows.Forms.ListBox();
            this.coords = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(938, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openImageToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openImageToolStripMenuItem
            // 
            this.openImageToolStripMenuItem.Name = "openImageToolStripMenuItem";
            this.openImageToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.openImageToolStripMenuItem.Text = "Open image";
            // 
            // addImageButton
            // 
            this.addImageButton.Location = new System.Drawing.Point(225, 141);
            this.addImageButton.Name = "addImageButton";
            this.addImageButton.Size = new System.Drawing.Size(26, 23);
            this.addImageButton.TabIndex = 2;
            this.addImageButton.Text = "+";
            this.addImageButton.UseVisualStyleBackColor = true;
            this.addImageButton.Click += new System.EventHandler(this.addImageButton_Click);
            // 
            // removeImageButton
            // 
            this.removeImageButton.Location = new System.Drawing.Point(193, 141);
            this.removeImageButton.Name = "removeImageButton";
            this.removeImageButton.Size = new System.Drawing.Size(26, 23);
            this.removeImageButton.TabIndex = 3;
            this.removeImageButton.Text = "-";
            this.removeImageButton.UseVisualStyleBackColor = true;
            // 
            // spritesListBox
            // 
            this.spritesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.spritesListBox.FormattingEnabled = true;
            this.spritesListBox.IntegralHeight = false;
            this.spritesListBox.Location = new System.Drawing.Point(12, 170);
            this.spritesListBox.Name = "spritesListBox";
            this.spritesListBox.Size = new System.Drawing.Size(239, 111);
            this.spritesListBox.TabIndex = 4;
            // 
            // imagePanel
            // 
            this.imagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imagePanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.imagePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imagePanel.Location = new System.Drawing.Point(257, 27);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(669, 467);
            this.imagePanel.TabIndex = 6;
            this.imagePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.imagePanel_Paint);
            this.imagePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imagePanel_MouseDown);
            this.imagePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imagePanel_MouseMove);
            this.imagePanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imagePanel_MouseUp);
            // 
            // imagesListBox
            // 
            this.imagesListBox.FormattingEnabled = true;
            this.imagesListBox.Location = new System.Drawing.Point(12, 27);
            this.imagesListBox.Name = "imagesListBox";
            this.imagesListBox.Size = new System.Drawing.Size(239, 108);
            this.imagesListBox.TabIndex = 1;
            this.imagesListBox.SelectedIndexChanged += new System.EventHandler(this.imagesListBox_SelectedIndexChanged);
            // 
            // coords
            // 
            this.coords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.coords.Location = new System.Drawing.Point(826, 500);
            this.coords.Name = "coords";
            this.coords.Size = new System.Drawing.Size(100, 23);
            this.coords.TabIndex = 7;
            this.coords.Text = "X: 0 Y: 0 W: 0 H: 0";
            this.coords.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 530);
            this.Controls.Add(this.coords);
            this.Controls.Add(this.imagePanel);
            this.Controls.Add(this.spritesListBox);
            this.Controls.Add(this.removeImageButton);
            this.Controls.Add(this.addImageButton);
            this.Controls.Add(this.imagesListBox);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem;
        private System.Windows.Forms.Button addImageButton;
        private System.Windows.Forms.Button removeImageButton;
        private System.Windows.Forms.ListBox spritesListBox;
        private System.Windows.Forms.Panel imagePanel;
        private System.Windows.Forms.ListBox imagesListBox;
        private System.Windows.Forms.Label coords;
    }
}

