namespace spriteman
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spritesListBox = new System.Windows.Forms.ListBox();
            this.imagePanel = new System.Windows.Forms.Panel();
            this.imagesListBox = new System.Windows.Forms.ListBox();
            this.coords = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imagesToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripAddImageButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripRemoveImageButton = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.imagesToolStrip.SuspendLayout();
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
            this.spritesListBox.SelectedIndexChanged += new System.EventHandler(this.spritesListBox_SelectedIndexChanged);
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
            this.coords.Location = new System.Drawing.Point(676, 500);
            this.coords.Name = "coords";
            this.coords.Size = new System.Drawing.Size(250, 23);
            this.coords.TabIndex = 7;
            this.coords.Text = "W: 0 H: 0 X: 0 Y: 0";
            this.coords.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.imagesToolStrip);
            this.panel1.Location = new System.Drawing.Point(12, 141);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(239, 22);
            this.panel1.TabIndex = 8;
            // 
            // imagesToolStrip
            // 
            this.imagesToolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.imagesToolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagesToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.imagesToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripAddImageButton,
            this.toolStripRemoveImageButton});
            this.imagesToolStrip.Location = new System.Drawing.Point(0, 0);
            this.imagesToolStrip.Name = "imagesToolStrip";
            this.imagesToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.imagesToolStrip.Size = new System.Drawing.Size(239, 22);
            this.imagesToolStrip.TabIndex = 0;
            // 
            // toolStripAddImageButton
            // 
            this.toolStripAddImageButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripAddImageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripAddImageButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripAddImageButton.Image")));
            this.toolStripAddImageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripAddImageButton.Name = "toolStripAddImageButton";
            this.toolStripAddImageButton.Size = new System.Drawing.Size(23, 19);
            this.toolStripAddImageButton.Text = "toolStripButton1";
            this.toolStripAddImageButton.Click += new System.EventHandler(this.toolStripAddImageButton_Click);
            // 
            // toolStripRemoveImageButton
            // 
            this.toolStripRemoveImageButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripRemoveImageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripRemoveImageButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripRemoveImageButton.Image")));
            this.toolStripRemoveImageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripRemoveImageButton.Name = "toolStripRemoveImageButton";
            this.toolStripRemoveImageButton.Size = new System.Drawing.Size(23, 19);
            this.toolStripRemoveImageButton.Text = "toolStripButton1";
            this.toolStripRemoveImageButton.Click += new System.EventHandler(this.toolStripRemoveImageButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 530);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.coords);
            this.Controls.Add(this.imagePanel);
            this.Controls.Add(this.spritesListBox);
            this.Controls.Add(this.imagesListBox);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Spriteman";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.imagesToolStrip.ResumeLayout(false);
            this.imagesToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem;
        private System.Windows.Forms.ListBox spritesListBox;
        private System.Windows.Forms.Panel imagePanel;
        private System.Windows.Forms.ListBox imagesListBox;
        private System.Windows.Forms.Label coords;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip imagesToolStrip;
        private System.Windows.Forms.ToolStripButton toolStripAddImageButton;
        private System.Windows.Forms.ToolStripButton toolStripRemoveImageButton;
    }
}

