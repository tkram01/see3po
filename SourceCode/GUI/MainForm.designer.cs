namespace See3PO
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDivider1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFloorPlanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFloorPlanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFloorPlanImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.connectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.livePanel = new System.Windows.Forms.Panel();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.floorPlanPanel = new System.Windows.Forms.Panel();
            this.importImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.Move = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 617);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1066, 22);
            this.statusStrip.TabIndex = 23;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(63, 17);
            this.statusLabel.Text = "status: idle";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.connectionMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1066, 24);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "mainMenu";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDivider1,
            this.exitMenuItem,
            this.loadFloorPlanToolStripMenuItem,
            this.saveFloorPlanToolStripMenuItem,
            this.importFloorPlanImageToolStripMenuItem});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "File";
            // 
            // menuDivider1
            // 
            this.menuDivider1.Name = "menuDivider1";
            this.menuDivider1.Size = new System.Drawing.Size(196, 6);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(199, 22);
            this.exitMenuItem.Text = "Exit";
            // 
            // loadFloorPlanToolStripMenuItem
            // 
            this.loadFloorPlanToolStripMenuItem.Name = "loadFloorPlanToolStripMenuItem";
            this.loadFloorPlanToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.loadFloorPlanToolStripMenuItem.Text = "Load FloorPlan";
            // 
            // saveFloorPlanToolStripMenuItem
            // 
            this.saveFloorPlanToolStripMenuItem.Name = "saveFloorPlanToolStripMenuItem";
            this.saveFloorPlanToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.saveFloorPlanToolStripMenuItem.Text = "Save FloorPlan";
            // 
            // importFloorPlanImageToolStripMenuItem
            // 
            this.importFloorPlanImageToolStripMenuItem.Name = "importFloorPlanImageToolStripMenuItem";
            this.importFloorPlanImageToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.importFloorPlanImageToolStripMenuItem.Text = "Import FloorPlan Image";
            this.importFloorPlanImageToolStripMenuItem.Click += new System.EventHandler(this.Import_Click);
            // 
            // connectionMenu
            // 
            this.connectionMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectMenuItem});
            this.connectionMenu.Name = "connectionMenu";
            this.connectionMenu.Size = new System.Drawing.Size(81, 20);
            this.connectionMenu.Text = "Connection";
            // 
            // connectMenuItem
            // 
            this.connectMenuItem.Name = "connectMenuItem";
            this.connectMenuItem.Size = new System.Drawing.Size(105, 22);
            this.connectMenuItem.Text = "Listen";
            this.connectMenuItem.Click += new System.EventHandler(this.connectMenuItem_Click);
            // 
            // livePanel
            // 
            this.livePanel.Location = new System.Drawing.Point(12, 27);
            this.livePanel.Name = "livePanel";
            this.livePanel.Size = new System.Drawing.Size(492, 428);
            this.livePanel.TabIndex = 25;
            // 
            // messageBox
            // 
            this.messageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.messageBox.BackColor = System.Drawing.Color.White;
            this.messageBox.Location = new System.Drawing.Point(12, 517);
            this.messageBox.Multiline = true;
            this.messageBox.Name = "messageBox";
            this.messageBox.ReadOnly = true;
            this.messageBox.Size = new System.Drawing.Size(1042, 87);
            this.messageBox.TabIndex = 58;
            // 
            // floorPlanPanel
            // 
            this.floorPlanPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.floorPlanPanel.Location = new System.Drawing.Point(510, 27);
            this.floorPlanPanel.Name = "floorPlanPanel";
            this.floorPlanPanel.Size = new System.Drawing.Size(544, 480);
            this.floorPlanPanel.TabIndex = 65;
            this.floorPlanPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.floorPlanPanel_Paint);
            this.floorPlanPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.setDestination);
            this.floorPlanPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.drawDestination);
            // 
            // importImageDialog
            // 
            this.importImageDialog.Filter = "\"IMAGES|*jpg;*bmp|All|*.*\";";
            this.importImageDialog.InitialDirectory = "\"\"";
            // 
            // Move
            // 
            this.Move.Location = new System.Drawing.Point(404, 464);
            this.Move.Name = "Move";
            this.Move.Size = new System.Drawing.Size(95, 37);
            this.Move.TabIndex = 66;
            this.Move.Text = "Move";
            this.Move.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 639);
            this.Controls.Add(this.Move);
            this.Controls.Add(this.floorPlanPanel);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.livePanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ViLAN Explorer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileMenu;
		private System.Windows.Forms.ToolStripSeparator menuDivider1;
		private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
		private System.Windows.Forms.Panel livePanel;
		private System.Windows.Forms.ToolStripMenuItem connectionMenu;
		private System.Windows.Forms.ToolStripMenuItem connectMenuItem;
        private System.Windows.Forms.TextBox messageBox;
		private System.Windows.Forms.Panel floorPlanPanel;
        private System.Windows.Forms.ToolStripMenuItem loadFloorPlanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFloorPlanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFloorPlanImageToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog importImageDialog;
        private System.Windows.Forms.Button Move;

	}
}

