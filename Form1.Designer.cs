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
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.kvpListView = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.propertiesToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripAddKvpButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripDeleteKvpButton = new System.Windows.Forms.ToolStripButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.spritesToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripDeleteSpriteButton = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.imagesToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kvpListView)).BeginInit();
            this.panel2.SuspendLayout();
            this.propertiesToolStrip.SuspendLayout();
            this.panel3.SuspendLayout();
            this.spritesToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(938, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.saveProjectToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "&File";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.newProjectToolStripMenuItem.Text = "&New project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.openProjectToolStripMenuItem.Text = "&Open project";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveProjectToolStripMenuItem.Text = "&Save project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(183, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
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
            this.spritesListBox.Location = new System.Drawing.Point(12, 201);
            this.spritesListBox.Name = "spritesListBox";
            this.spritesListBox.Size = new System.Drawing.Size(239, 164);
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
            this.imagePanel.Location = new System.Drawing.Point(257, 52);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(669, 495);
            this.imagePanel.TabIndex = 6;
            this.imagePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.imagePanel_Paint);
            this.imagePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imagePanel_MouseDown);
            this.imagePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imagePanel_MouseMove);
            this.imagePanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imagePanel_MouseUp);
            // 
            // imagesListBox
            // 
            this.imagesListBox.FormattingEnabled = true;
            this.imagesListBox.IntegralHeight = false;
            this.imagesListBox.Location = new System.Drawing.Point(12, 52);
            this.imagesListBox.Name = "imagesListBox";
            this.imagesListBox.Size = new System.Drawing.Size(239, 102);
            this.imagesListBox.TabIndex = 1;
            this.imagesListBox.SelectedIndexChanged += new System.EventHandler(this.imagesListBox_SelectedIndexChanged);
            // 
            // coords
            // 
            this.coords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.coords.Location = new System.Drawing.Point(676, 553);
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
            this.panel1.Location = new System.Drawing.Point(12, 160);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Source images";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Sprites";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 396);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Properties";
            // 
            // kvpListView
            // 
            this.kvpListView.AllColumns.Add(this.olvColumn1);
            this.kvpListView.AllColumns.Add(this.olvColumn2);
            this.kvpListView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kvpListView.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.kvpListView.CellEditUseWholeCell = false;
            this.kvpListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2});
            this.kvpListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.kvpListView.FullRowSelect = true;
            this.kvpListView.GridLines = true;
            this.kvpListView.HasCollapsibleGroups = false;
            this.kvpListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.kvpListView.HideSelection = false;
            this.kvpListView.Location = new System.Drawing.Point(12, 412);
            this.kvpListView.Name = "kvpListView";
            this.kvpListView.ShowGroups = false;
            this.kvpListView.Size = new System.Drawing.Size(239, 104);
            this.kvpListView.TabIndex = 0;
            this.kvpListView.UseCompatibleStateImageBehavior = false;
            this.kvpListView.UseNotifyPropertyChanged = true;
            this.kvpListView.View = System.Windows.Forms.View.Details;
            this.kvpListView.CellEditFinished += new BrightIdeasSoftware.CellEditEventHandler(this.kvpListView_CellEditFinished);
            this.kvpListView.SelectedIndexChanged += new System.EventHandler(this.kvpListView_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Key";
            this.olvColumn1.Text = "Key";
            this.olvColumn1.Width = 120;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Value";
            this.olvColumn2.FillsFreeSpace = true;
            this.olvColumn2.Text = "Value";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.propertiesToolStrip);
            this.panel2.Location = new System.Drawing.Point(12, 522);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(239, 22);
            this.panel2.TabIndex = 13;
            // 
            // propertiesToolStrip
            // 
            this.propertiesToolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.propertiesToolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.propertiesToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripAddKvpButton,
            this.toolStripDeleteKvpButton});
            this.propertiesToolStrip.Location = new System.Drawing.Point(0, 0);
            this.propertiesToolStrip.Name = "propertiesToolStrip";
            this.propertiesToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.propertiesToolStrip.Size = new System.Drawing.Size(239, 22);
            this.propertiesToolStrip.TabIndex = 0;
            // 
            // toolStripAddKvpButton
            // 
            this.toolStripAddKvpButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripAddKvpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripAddKvpButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripAddKvpButton.Image")));
            this.toolStripAddKvpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripAddKvpButton.Name = "toolStripAddKvpButton";
            this.toolStripAddKvpButton.Size = new System.Drawing.Size(23, 19);
            this.toolStripAddKvpButton.Text = "toolStripButton1";
            this.toolStripAddKvpButton.Click += new System.EventHandler(this.toolStripAddKvpButton_Click);
            // 
            // toolStripDeleteKvpButton
            // 
            this.toolStripDeleteKvpButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripDeleteKvpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDeleteKvpButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDeleteKvpButton.Image")));
            this.toolStripDeleteKvpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDeleteKvpButton.Name = "toolStripDeleteKvpButton";
            this.toolStripDeleteKvpButton.Size = new System.Drawing.Size(23, 19);
            this.toolStripDeleteKvpButton.Text = "toolStripButton1";
            this.toolStripDeleteKvpButton.Click += new System.EventHandler(this.toolStripDeleteKvpButton_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.spritesToolStrip);
            this.panel3.Location = new System.Drawing.Point(12, 371);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(239, 22);
            this.panel3.TabIndex = 14;
            // 
            // spritesToolStrip
            // 
            this.spritesToolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.spritesToolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spritesToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.spritesToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDeleteSpriteButton});
            this.spritesToolStrip.Location = new System.Drawing.Point(0, 0);
            this.spritesToolStrip.Name = "spritesToolStrip";
            this.spritesToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.spritesToolStrip.Size = new System.Drawing.Size(239, 22);
            this.spritesToolStrip.TabIndex = 0;
            // 
            // toolStripDeleteSpriteButton
            // 
            this.toolStripDeleteSpriteButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripDeleteSpriteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDeleteSpriteButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDeleteSpriteButton.Image")));
            this.toolStripDeleteSpriteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDeleteSpriteButton.Name = "toolStripDeleteSpriteButton";
            this.toolStripDeleteSpriteButton.Size = new System.Drawing.Size(23, 19);
            this.toolStripDeleteSpriteButton.Text = "toolStripButton1";
            this.toolStripDeleteSpriteButton.Click += new System.EventHandler(this.toolStripDeleteSpriteButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 583);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.kvpListView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.imagesToolStrip.ResumeLayout(false);
            this.imagesToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kvpListView)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.propertiesToolStrip.ResumeLayout(false);
            this.propertiesToolStrip.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.spritesToolStrip.ResumeLayout(false);
            this.spritesToolStrip.PerformLayout();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private BrightIdeasSoftware.ObjectListView kvpListView;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip propertiesToolStrip;
        private System.Windows.Forms.ToolStripButton toolStripAddKvpButton;
        private System.Windows.Forms.ToolStripButton toolStripDeleteKvpButton;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip spritesToolStrip;
        private System.Windows.Forms.ToolStripButton toolStripDeleteSpriteButton;
    }
}

