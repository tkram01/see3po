namespace Host
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
            this.components = new System.ComponentModel.Container();
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
            this.floorPlanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.placeRobotToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.setDestinationToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.driveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.livePanel = new System.Windows.Forms.Panel();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.floorPlanPanel = new System.Windows.Forms.Panel();
            this.importImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.floorPlanContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loadFloorPlanToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFloorPlanToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.importFloorPlanImageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.placeRobotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDestinationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.floorPlanContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 640);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1084, 22);
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
            this.connectionMenu,
            this.floorPlanToolStripMenuItem,
            this.driveToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1084, 24);
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
            this.importFloorPlanImageToolStripMenuItem.Click += new System.EventHandler(this.Click_Import);
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
            this.connectMenuItem.Click += new System.EventHandler(this.ConnectMenuItem_Click);
            // 
            // floorPlanToolStripMenuItem
            // 
            this.floorPlanToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setScaleToolStripMenuItem,
            this.placeRobotToolStripMenuItem1,
            this.setDestinationToolStripMenuItem1});
            this.floorPlanToolStripMenuItem.Name = "floorPlanToolStripMenuItem";
            this.floorPlanToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.floorPlanToolStripMenuItem.Text = "FloorPlan";
            // 
            // setScaleToolStripMenuItem
            // 
            this.setScaleToolStripMenuItem.Name = "setScaleToolStripMenuItem";
            this.setScaleToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.setScaleToolStripMenuItem.Text = "Set Scale";
            this.setScaleToolStripMenuItem.Click += new System.EventHandler(this.Click_SetScale);
            // 
            // placeRobotToolStripMenuItem1
            // 
            this.placeRobotToolStripMenuItem1.Name = "placeRobotToolStripMenuItem1";
            this.placeRobotToolStripMenuItem1.Size = new System.Drawing.Size(153, 22);
            this.placeRobotToolStripMenuItem1.Text = "Place Robot";
            this.placeRobotToolStripMenuItem1.Click += new System.EventHandler(this.Click_PlaceRobot);
            // 
            // setDestinationToolStripMenuItem1
            // 
            this.setDestinationToolStripMenuItem1.Name = "setDestinationToolStripMenuItem1";
            this.setDestinationToolStripMenuItem1.Size = new System.Drawing.Size(153, 22);
            this.setDestinationToolStripMenuItem1.Text = "Set Destination";
            this.setDestinationToolStripMenuItem1.Click += new System.EventHandler(this.Click_SetDestination);
            // 
            // driveToolStripMenuItem
            // 
            this.driveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goMenuItem,
            this.showPathToolStripMenuItem,
            this.manualToolStripMenuItem});
            this.driveToolStripMenuItem.Name = "driveToolStripMenuItem";
            this.driveToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.driveToolStripMenuItem.Text = "Drive";
            // 
            // goMenuItem
            // 
            this.goMenuItem.Name = "goMenuItem";
            this.goMenuItem.Size = new System.Drawing.Size(152, 22);
            this.goMenuItem.Text = "Go";
            this.goMenuItem.Click += new System.EventHandler(this.goMenuItem_Click);
            // 
            // manualToolStripMenuItem
            // 
            this.manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            this.manualToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.manualToolStripMenuItem.Text = "Manual";
            // 
            // livePanel
            // 
            this.livePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.livePanel.Location = new System.Drawing.Point(0, 0);
            this.livePanel.Name = "livePanel";
            this.livePanel.Size = new System.Drawing.Size(360, 352);
            this.livePanel.TabIndex = 25;
            // 
            // messageBox
            // 
            this.messageBox.BackColor = System.Drawing.Color.White;
            this.messageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageBox.Location = new System.Drawing.Point(0, 0);
            this.messageBox.Multiline = true;
            this.messageBox.Name = "messageBox";
            this.messageBox.ReadOnly = true;
            this.messageBox.Size = new System.Drawing.Size(360, 260);
            this.messageBox.TabIndex = 58;
            // 
            // floorPlanPanel
            // 
            this.floorPlanPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.floorPlanPanel.Location = new System.Drawing.Point(0, 0);
            this.floorPlanPanel.Name = "floorPlanPanel";
            this.floorPlanPanel.Size = new System.Drawing.Size(720, 616);
            this.floorPlanPanel.TabIndex = 65;
            this.floorPlanPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.Paint_FloorPlanPanel);
            this.floorPlanPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SetDestination);
            this.floorPlanPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Click_FloorPlan);
            // 
            // importImageDialog
            // 
            this.importImageDialog.Filter = "\"IMAGES|*jpg;*bmp|All|*.*\";";
            this.importImageDialog.InitialDirectory = "\"\"";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.floorPlanPanel);
            this.splitContainer1.Size = new System.Drawing.Size(1084, 616);
            this.splitContainer1.SplitterDistance = 360;
            this.splitContainer1.TabIndex = 66;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.livePanel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.messageBox);
            this.splitContainer2.Size = new System.Drawing.Size(360, 616);
            this.splitContainer2.SplitterDistance = 352;
            this.splitContainer2.TabIndex = 0;
            // 
            // floorPlanContext
            // 
            this.floorPlanContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadFloorPlanToolStripMenuItem1,
            this.saveFloorPlanToolStripMenuItem1,
            this.importFloorPlanImageToolStripMenuItem1,
            this.addToolStripMenuItem,
            this.placeRobotToolStripMenuItem,
            this.setDestinationToolStripMenuItem});
            this.floorPlanContext.Name = "contextMenuStrip1";
            this.floorPlanContext.Size = new System.Drawing.Size(203, 136);
            // 
            // loadFloorPlanToolStripMenuItem1
            // 
            this.loadFloorPlanToolStripMenuItem1.Name = "loadFloorPlanToolStripMenuItem1";
            this.loadFloorPlanToolStripMenuItem1.Size = new System.Drawing.Size(202, 22);
            this.loadFloorPlanToolStripMenuItem1.Text = "Load Floor Plan";
            // 
            // saveFloorPlanToolStripMenuItem1
            // 
            this.saveFloorPlanToolStripMenuItem1.Name = "saveFloorPlanToolStripMenuItem1";
            this.saveFloorPlanToolStripMenuItem1.Size = new System.Drawing.Size(202, 22);
            this.saveFloorPlanToolStripMenuItem1.Text = "Save Floor Plan";
            // 
            // importFloorPlanImageToolStripMenuItem1
            // 
            this.importFloorPlanImageToolStripMenuItem1.Name = "importFloorPlanImageToolStripMenuItem1";
            this.importFloorPlanImageToolStripMenuItem1.Size = new System.Drawing.Size(202, 22);
            this.importFloorPlanImageToolStripMenuItem1.Text = "Import Floor Plan Image";
            this.importFloorPlanImageToolStripMenuItem1.Click += new System.EventHandler(this.Click_Import);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.addToolStripMenuItem.Text = "Set Scale";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.Click_SetScale);
            // 
            // placeRobotToolStripMenuItem
            // 
            this.placeRobotToolStripMenuItem.Name = "placeRobotToolStripMenuItem";
            this.placeRobotToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.placeRobotToolStripMenuItem.Text = "Place Robot";
            this.placeRobotToolStripMenuItem.Click += new System.EventHandler(this.Click_PlaceRobot);
            // 
            // setDestinationToolStripMenuItem
            // 
            this.setDestinationToolStripMenuItem.Name = "setDestinationToolStripMenuItem";
            this.setDestinationToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.setDestinationToolStripMenuItem.Text = "Set Destination";
            this.setDestinationToolStripMenuItem.Click += new System.EventHandler(this.Click_SetDestination);
            // 
            // showPathToolStripMenuItem
            // 
            this.showPathToolStripMenuItem.Name = "showPathToolStripMenuItem";
            this.showPathToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.showPathToolStripMenuItem.Text = "Show Path";
            this.showPathToolStripMenuItem.Click += new System.EventHandler(this.Click_ShowPath);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 662);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ViLAN Explorer";
            this.Load += new System.EventHandler(this.Load_MainForm);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.floorPlanContext.ResumeLayout(false);
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
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripMenuItem floorPlanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setScaleToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip floorPlanContext;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFloorPlanToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveFloorPlanToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem importFloorPlanImageToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem placeRobotToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem setDestinationToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem placeRobotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setDestinationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem driveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPathToolStripMenuItem;

	}
}

