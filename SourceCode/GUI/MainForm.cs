using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using System.Drawing.Drawing2D;
using See3PO;
using Nexus3Input;
using Timer = System.Threading.Timer;
using FloorTile = See3PO.FloorTile;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GUI
{ 
	public partial class MainForm : Form, See3PO.UI
	{

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            m_host = new See3PO.Host(this);                                     // Create a Host

            m_center = new Point(floorPlanPanel.Width / 2, floorPlanPanel.Height / 2); //find the center of the image
            
            m_fg = Graphics.FromHwnd(floorPlanPanel.Handle);                    // create a graphics object to draw on the floorplanpanel    
            
            m_scale = 2.0;                                                      // default ppf
            
            m_callback = new TimerCallback(DrawScale);                          // I have no idea why we do this, but 
            t_DrawFloorDelegate = new DDrawFloor(DrawFloor);                    //it's necessary for the drawscale thread
                                
            m_fpState = fpState.START;                                           // Set the fpState to none, since nothing has been done
            Click_ConnectMenuItem(this, null);
        }

//************************************************************************************************
//       Public Methods
//************************************************************************************************

        /// <summary>
        /// Selects a point to highlight and sets the flag to true.
        /// Used by the manual pathfinder
        /// </summary>
        /// <param name="point"></param>
        public void highlightPoint(Point point)
        {
            m_highlighted = true;

            m_highlight = FloorPlanToPanel(point);

            DrawFloor();
        }

        public void DrawScale() 
        {
            m_fpState = fpState.NOSCALE;
            DrawFloor();

        }

        /// <summary>
        /// Disconnects the host from the robot before closing
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            m_host.OnClosing();                                                 // Let the host disconnect

            base.OnClosing(e);
        }

        /// <summary>
        /// Posts connection status to the screen
        /// </summary>
        /// <param name="msg"></param>
        public void PostConnection(String msg)
        {
            if (msg == "Connected")
                connectMenuItem.Text = ("Disconnect");
            if (msg == "Disconnected")
                connectMenuItem.Text = ("Listen");
            statusLabel.Text = msg;                                             // Update Connection Messages
        }

        /// <summary>
        /// Posts any message to the screen
        /// </summary>
        /// <param name="msg"></param>
        public void PostMessage(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new DGuiCallString(PostMessage), msg);
                return;
            }

            messageBox.Text = msg + "\r\n" + messageBox.Text;
        }

        /// <summary>
        /// Posts any message to the screen
        /// </summary>
        /// <param name="msg"></param>
        public void PostImage(Image img)
        {
            if (InvokeRequired)
            {
                Invoke(new DGuiCallImg(PostImage), img);
                return;
            }

            Graphics g = Graphics.FromHwnd(livePanel.Handle);
            g.DrawImage(img, 0, 0, livePanel.Width, livePanel.Height);
        }

//************************************************************************************************
//       Menu Clicks
//************************************************************************************************

        /// <summary>
        /// Result of clicking the "Connect" menu item - connect the host to the robot
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void Click_ConnectMenuItem(object sender, EventArgs e)
        {
            m_host.ToggleConnection();
        }
        
        /// <summary>
        /// Result of clicking the FloorPlanPanel - does different things depending on fpState and the button clicked
        /// </summary>
        /// <param name="e">mouse position</param>
        private void Click_FloorPlan(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:                                         // If we're dealing with a normal click
                    switch (m_fpState)
                    {
                        case fpState.NOSCALE:                                     // We've loaded the image, so this is to set the scale
                            m_scaleStart = e.Location;                          // Set the starting point then go into DrawScale mode
                            m_fpState = fpState.DRAWSCALE;
                            t_DrawTimer = new Timer(m_callback, null, 0, 100);
                            break;

                        case fpState.DRAWSCALE:                                 // We're currently drawing the scale, so this is the endpoint
                            m_scaleEnd = e.Location;                            // Set the endpoint and then call SetScale method
                            double scaleLength = Length(m_scaleStart.X - m_scaleEnd.X, m_scaleStart.Y - m_scaleEnd.Y);
                            if (t_DrawTimer != null)
                            {
                                t_DrawTimer.Dispose();                                     // If there's a drawscale timer running, kill it
                            }
                            SetScale(scaleLength);
                            break;

                        case fpState.SETROBOT:                                  // We're setting the rotob's position
                            PlaceRobot(sender, e);
                            new FacingForm(this).Show();
                            //m_fpState = fpState.SETFACING;
                            //t_DrawTimer = new Timer(m_callback, null, 0, 100);
                            break;
                        case fpState.SETFACING:                                 // Once we plae the robot, we need to set its current facing direction
                            m_fpState = fpState.SETDEST;
                            Point panelPosition = (FloorPlanToPanel(m_host.Status.Position.location));
                            Point mousePosition = e.Location;
                            m_host.Status.Position.facing = SlopeToFacing(panelPosition, mousePosition);

                            if (t_DrawTimer != null)
                            {
                                t_DrawTimer.Dispose();                                     // If there's a drawscale timer running, kill it
                            }
                            DrawFloor();
                            break;

                        case fpState.SETDEST:                                   // If we have a destination or path
                        case fpState.HAVEPATH:                                  // Then we're just setting new destinations
                            if (m_host.Status != null)
                            {
                                SetDestination(sender, e);
                            }
                            break;
                    }
                    break;

                case MouseButtons.Right:                                        // If it's a right-click, show the context menu
                    floorPlanContext.Show(System.Windows.Forms.Control.MousePosition);
                    break;

                case MouseButtons.Middle:
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Result of clicking the "GO" button - Sends the Robot moving 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_GoMenuItem(object sender, EventArgs e)
        {
            if ( m_fpState == fpState.HAVEPATH && m_host.Status != null && m_host.Status.Path != null)
            {
                m_host.Drive();
            }
        }

        /// <summary>
        /// Result of clicking the "Import" Menu item - prompts the user to load a floor plan 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Import(object sender, EventArgs e)
        {
            importImageDialog.InitialDirectory = ".\floorplan";                                 // Look in the floorplan directory

            importImageDialog.ShowDialog();                                                     // Show the file dialogue                                                                        // draw the new floor plan
        }

        /// <summary>
        /// Result of clicking the "Place Robot" menu option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_PlaceRobot(object sender, EventArgs e)
        {
            if (m_fpState == fpState.SETDEST || m_fpState == fpState.HAVEPATH || m_fpState == fpState.SETROBOT)
            {
                m_fpState = fpState.SETROBOT;
                DrawFloor();
            }
        }

        private void Click_SetDestination(object sender, EventArgs e)
        {
            if (m_fpState == fpState.SETDEST || m_fpState == fpState.HAVEPATH || m_fpState == fpState.SETROBOT)
            {
                m_fpState = fpState.SETDEST;
                DrawFloor();
            }
        }

        /// <summary>
        /// Result of clicking the "Set Scale" menu option
        /// </summary>
        private void Click_SetScale(object sender, EventArgs e)
        {
            SetScale(10.0);
        }

        /// <summary>
        /// Result of clicking the "Show Path" menu option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_ShowPath(object sender, EventArgs e)
        {
            PathForm pathform = new PathForm(this);
            m_host.Locator = pathform;                                          // This will act as the Locator while it's running
            pathform.Show();
        }

        /// <summary>
        /// Updates the UI - called from the host
        /// </summary>
        public void updateUI()
        {
            DrawFloor();
        }



//************************************************************************************************
//       Private Methods
//************************************************************************************************



        /// <summary>
        /// Draws the floorPlan, path and locations to the FloorPlanPanel
        /// </summary>
        public void DrawFloor()
        {
            try
            {
                Bitmap buffer = new Bitmap(floorPlanPanel.Width, floorPlanPanel.Height);        // We'll draw to this, then draw this to the panel

                Graphics bg = Graphics.FromImage(buffer);                                       // graphics object for the buffer

                SolidBrush overlay = new SolidBrush(Color.FromArgb(0, Color.White));            // A highlight color to indicate the state

                String instructions = "";                                                       // Instructions based on the current state

                switch (m_fpState)
                {
                    case fpState.START:                                                          // No floorplan even exists - don't draw anything
                        instructions = "Load or Import a Floor Plan";
                        break;

                    case fpState.NOSCALE:                                                         // The image has been loaded, but not converted
                        instructions = "Click on the Floor or Select \"Draw Scale\" from the tools menu";
                        bg.DrawImage(m_floorPlanImage, 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                        overlay = new SolidBrush(Color.FromArgb(10, Color.Green));
                        break;

                    case fpState.DRAWSCALE:                                                     // We're currently drawing the floorplan's scale
                        instructions = "Click again to draw a known measurement";
                        bg.DrawImage(m_floorPlanImage, 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                        overlay = new SolidBrush(Color.FromArgb(10, Color.Green));
                        bg.DrawLine(new Pen(Color.Blue), (PointF)m_scaleStart,
                            floorPlanPanel.PointToClient(System.Windows.Forms.Control.MousePosition));
                        break;

                    case fpState.SETROBOT:                                                      // We're placing the robot by hand       
                        bg.DrawImage(m_host.Status.FloorPlan.toImage( floorPlanPanel.Width, floorPlanPanel.Height), 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                        instructions = "Click the floor plan to set the robot's current location";
                        overlay = new SolidBrush(Color.FromArgb(10, Color.Red));
                        break;

                    case fpState.SETFACING:
                        instructions = "Draw your line to indicate facing: Current FAcing ";
                        bg.DrawImage(m_host.Status.FloorPlan.toImage(floorPlanPanel.Width, floorPlanPanel.Height), 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                        overlay = new SolidBrush(Color.FromArgb(10, Color.Green));
                        Point panelPosition = (FloorPlanToPanel(m_host.Status.Position.location));
                        Point mousePosition = floorPlanPanel.PointToClient(System.Windows.Forms.Control.MousePosition);
                        bg.DrawEllipse(new Pen(new SolidBrush(Color.Red)), panelPosition.X, panelPosition.Y, 1, 1);
                        bg.DrawLine(new Pen(Color.Blue), (PointF)panelPosition,
                            floorPlanPanel.PointToClient(System.Windows.Forms.Control.MousePosition));
                        instructions = "CurrentFacing: " + SlopeToFacing(panelPosition, mousePosition);
                        break;

                    case fpState.SETDEST:
                        instructions = "Click the floor plan to set the destination";           // We're choosing a destination
                        bg.DrawImage(m_host.Status.FloorPlan.toImage(floorPlanPanel.Width, floorPlanPanel.Height), 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                        break;

                    case fpState.HAVEPATH:
                        instructions = "Click the floor plan to change the destination";        // We have a path and are ready to drive
                        bg.DrawImage(m_host.Status.FloorPlan.toImage(floorPlanPanel.Width, floorPlanPanel.Height), 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                        if (m_host.Status.Path != null) { }
                        //   bg.DrawImage(DrawPath(), 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                        else
                            instructions = "unwalkable destination";
                        break;
                }
                bg.FillRectangle(overlay, 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);   // draw the overlay
                bg.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit; // stretch the pixels
                m_fg = Graphics.FromHwnd(floorPlanPanel.Handle);
                m_fg.DrawImage(buffer, 0, 0);                                                   // Draw the entire image to the panel
                InstructionsLabel.Text = instructions;                                          // display instructions
            }
            catch (InvalidOperationException e) { PostMessage(e.ToString()); };
        }

        /// <summary>
        /// This Draws the Path whenever an update occurs.
        /// </summary>
        /// <returns></returns>
        private Image DrawPath()
        {
            Bitmap trail = new Bitmap(floorPlanPanel.Width, floorPlanPanel.Height); // clear bitmap will have the trail drawn on it

            Graphics s = Graphics.FromImage(trail);                             // Graphic object to draw onto the bitmap

            Point lineStart = new Point(FloorPlanToPanel(m_host.Status.Position.location).X, 
                FloorPlanToPanel(m_host.Status.Position.location).Y);           // Starting Point is the robot's current location

            Point lineEnd = new Point(lineStart.X, lineStart.Y);                // For now, set the endpoint to the startpoint

            foreach (FloorTile tile in m_host.Status.Path)
            {

                lineStart = lineEnd;                                            // Move the old startpoint up to the new 

                lineEnd = FloorPlanToPanel(tile.Position);                      // Set the new endpoint to the next waypoint

                s.DrawLine(new Pen(new SolidBrush(Color.Blue),2), lineStart, lineEnd); // Draw the line
            }

            if (m_highlighted)
                s.DrawEllipse(new Pen(new SolidBrush(Color.BlueViolet), 2),     // If there's a highted point, highlight it
                    new Rectangle(CenterPoint(m_highlight, 4), new Size(3, 3)));


            return trail;                                                       // Return it to the DrawFloor method 
        }

        /// <summary>
        /// Runs in a loop which redraws the background to allow the user to draw a scale line
        /// </summary>
        /// <param name="State">not used</param>
        private void DrawScale(object State)
        {
            if (InvokeRequired)
            {
                Invoke(new DDrawFloor(DrawFloor), null);
                return;
            }
        }

        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Load_MainForm(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Draws the floor plan, not directly used
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Paint_FloorPlanPanel(object sender, PaintEventArgs e)
        {
            DrawFloor();
        }

        private Point PanelToFloorPlan(Point panelPoint)
        {
            return new Point((int)(panelPoint.X * m_ratioX), (int)(panelPoint.Y * m_ratioY));
        }

        /// <summary>
        /// Move's the robot's current position by mouse event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">mouse location on the FloorPlanPanel</param>
        private void PlaceRobot(object sender, MouseEventArgs e)
        {
            PlaceRobot(e.Location, m_host.Status.Position.facing);
        }

        /// <summary>
        /// Move's the robot's position
        /// </summary>
        /// <param name="loc">Location on the floorplanpanel</param>
        /// <param name="facing"></param>
        public void PlaceRobot(Point loc, int facing)
        {
            if (m_host.Status != null)
            {
                m_host.Status.Position = new Position(PanelToFloorPlan(loc), facing);
                if (m_host.Status.Path != null)
                    m_fpState = fpState.HAVEPATH;
                else
                    m_fpState = fpState.SETDEST;
                DrawFloor();
            }
        }

        /// <summary>
        /// Set's the robot's destination
        /// </summary>
        private void SetDestination(object sender, MouseEventArgs e)
        {
            m_host.SetDestination(PanelToFloorPlan(new Point(e.X, e.Y)));       // Set the endpoint in the Host's status
            m_fpState = fpState.HAVEPATH;                                       // Update the state
            DrawFloor();                                                        // Draw the floor
        }

        /// <summary>
        /// Sets the scale and creates a new floorplan in m_host, ending the DrawScale loop
        /// </summary>
        /// <param name="scaleLength">the length of the line drawn</param>
        private void SetScale(double scaleLength)
        {
            using (ScaleForm sf = new ScaleForm(scaleLength, scaleLength / m_scale, this))
            {
                sf.ShowDialog();                                                // Show the ScaleForm

                if (m_fpState == fpState.SETDEST)
                {
                    m_scale = sf.m_scale;                                           // When we return, get the scale from the form

                    m_host.CreateStatus(m_floorPlanImage, m_scale);              // Create a new floorplan

                    m_ratioX = (double)(m_host.Status.FloorPlan.getXTileNum()) / (double)(floorPlanPanel.Width); // figure out the ratio of the size of
                    m_ratioY = (double)m_host.Status.FloorPlan.getYTileNum() / (double)floorPlanPanel.Height; // the floorplan to the panel
                }
                
            }                                                                 

            DrawFloor();                                                        // Draw the floor
        }



//************************************************************************************************
//       Functions
//************************************************************************************************

        /// <summary>
        /// Centers an image around a point (by moving it up and to the left)
        /// </summary>
        /// <param name="original">original point</param>
        /// <param name="size">the image</param>
        /// <returns>new location of the upper left corner of the image</returns>
        private Point CenterPointOnImage(Point original, Image image)
        {
            return new Point(original.X - (image.Width / 4), original.Y - (image.Height / 4));
        }

        /// <summary>
        /// Centers a shape around a point (by moving it up and to the left)
        /// </summary>
        /// <param name="original">original point</param>
        /// <param name="size">the size of the shape</param>
        /// <returns>new location fo the upper left corner of the shape</returns>
        private Point CenterPoint(Point original, int size)
        {
            return new Point(original.X - (size / 2), original.Y - (size / 2));
        }

        /// <summary>
        /// Converts A point in FloorPlan Coordinates to FloorPlanPanel Coordinates, 
        /// in case they're different sizes
        /// </summary>
        /// <param name="fpPoint">Point, in Floor Plan Coords</param>
        /// <returns>Point, in FloorPlanPanel Coords</returns>
        private Point FloorPlanToPanel(Point fpPoint)
        {
            return new Point((int)(fpPoint.X / m_ratioX), (int)(fpPoint.Y / m_ratioY));
        }

        /// <summary>
        /// Converts an iteger to a byte array
        /// </summary>
        /// <param name="source">original int</param>
        /// <returns>two bytes holding that int</returns>
        private byte[] IntToBytes(int source)
        {
            byte[] bytes = new byte[2];

            bytes[1] = (byte)(source & 0xFF); //low

            bytes[0] = (byte)((source >> 8) & 0xFF); //high

            return bytes;
        }

        /// <summary>
        /// Gets the hypoteneus from the sides
        /// </summary>
        /// <param name="x">one side</param>
        /// <param name="y">the other side!</param>
        /// <returns>the Hypoteneus</returns>
        private double Length(double x, double y)
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        private int SlopeToFacing(PointF start, PointF end) 
        {
            int facing = start.Y > end.Y ? 0 : 180;


            try
            {

               int angle = (int)(Math.Atan2((double)(end.X - start.Y), (double)(start.X - end.X)) * (180.0d / Math.PI));

               facing += start.Y > end.Y ? 180 - angle : angle; ;
            }
            catch (DivideByZeroException) { }

            return facing;
        }

        //************************************************************************************************
        //       attributes
        //************************************************************************************************
        public enum fpState                        // Various States that the FloorPlan could be in
        { START, NOSCALE, DRAWSCALE, SETROBOT, SETFACING, SETDEST, HAVEPATH }; // Used for drawing the floorplan

        private See3PO.Host m_host;                  // The Host program 

        private fpState m_fpState;                  // The Current fpState

        private Bitmap m_floorPlanImage;            // The original image of the floorplan

        delegate void DGuiCallString(string str);
        delegate void DGuiCallImg(Image img);

        delegate void DDrawFloor();                 // Delegate for the DrawFloor thread
        private DDrawFloor t_DrawFloorDelegate;

        private Timer t_DrawTimer;             // A timer for drawing the line when setting scale
        private TimerCallback m_callback;           // The TimerCallBack for drawing the line for setting the scale

        private Graphics m_fg;                      // graphics object for drawing the floorplan

        private Point m_center;                     // the center of the floorplan
        private Point m_highlight;                  // currently highlighted tile
        private bool m_highlighted;                 // true if there is a highlighted tile

        private Point m_scaleStart;         // The Start Point for the scale line
        private Point m_scaleEnd;           // The end point of the scale line
        private double m_scale;             // The scale - measured in pixels per foot

        private double m_ratioX;                    // The ratio of FloorPlan size to the size of the Panel displaying the floor plan
        private double m_ratioY;                    //  ratios in X and Y, respectively

        private CWebcam m_camera;

        //************************************************************************************************
        //       Getters and Setters for private attributes
        //************************************************************************************************
        
        /// <summary>
        /// Highlights a point on the map
        /// </summary>
        public Point Highlight
        {
            set 
            { 
                m_highlight = value;
                m_highlighted = true;
            }
        }

        /// <summary>
        /// true if there is a point to highlight
        /// </summary>
        public bool isHighlighted
        {
            set { m_highlighted = value; }
        }

        /// <summary>
        /// the Host's status
        /// </summary>
        public Status Status 
        {
            get { return m_host.Status; }
        }

        /// <summary>
        /// The FPState object, holding the current state of the floor plan
        /// </summary>
        public fpState FPState 
        {
            get { return m_fpState; }
            set { m_fpState = value; }
        }

        /// <summary>
        /// The scale of the Floor Plan, pixels to feet
        /// </summary>
        public double FPScale 
        {
            get { return m_scale; }
            set { m_scale = value; }
        }

        /// <summary>
        /// The Host
        /// </summary>
        public Host Host 
        { 
            get { return m_host; } 
        }

        private void onResize(object sender, EventArgs e)
        {
            DrawFloor();
        }

        private void loadFloorPlanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFloorPlanDialog.InitialDirectory = ".\floorplan";                                 // Look in the floorplan directory
            openFloorPlanDialog.ShowDialog();                                                     // Show the file dialogue  
        }                     

        private void saveFloorPlanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFloorPlanDialog.InitialDirectory = ".\floorplan";                                 // Look in the floorplan directory
            saveFloorPlanDialog.ShowDialog();                                                     // Show the file dialogue  
        } 

        private void saveFloorPlanDialog_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                Stream stream = File.Open(saveFloorPlanDialog.FileName, FileMode.Create);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(stream, m_host.Status.FloorPlan);
                stream.Close();
            }
            catch (Exception) { }
        }

        private void openFloorPlanDialog_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                FloorPlan fp;
                Stream stream = File.Open(openFloorPlanDialog.FileName, FileMode.Open);
                BinaryFormatter bFormatter = new BinaryFormatter();
                fp = (FloorPlan)bFormatter.Deserialize(stream);
                stream.Close();
                m_host.CreateStatus(fp);
            }
            catch (Exception) { }
        }

        private void importImageDialog_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                m_floorPlanImage = new Bitmap(Image.FromFile(importImageDialog.FileName));      // hopefully, they opened an image
                m_fpState = fpState.NOSCALE;                                                    // The state changes to reflecft our new image

                SetScale(10);                                                                   // Call set scale automatically - we can remove this
            }
            catch (Exception) { }
            DrawFloor();  
        }

	}
}
//Point[] currentDir = new Point[1];// get starting facing : 0 = East, 90 = North, 180 = West, 270 = South
//if (m_host.Status.position.facing > 315 || m_host.Status.position.facing <= 45)
//    currentDir[0] = new Point(1, 0);
//else if (m_host.Status.position.facing > 45 && m_host.Status.position.facing <= 135)
//    currentDir[0] = new Point(0, -1);
//else if (m_host.Status.position.facing > 135 && m_host.Status.position.facing <= 225)
//    currentDir[0] = new Point(-1, 0);
//else if (m_host.Status.position.facing > 225 && m_host.Status.position.facing <= 315)
//    currentDir[0] = new Point(0, 1);

//// Rotation Matrices
//Matrix CW = new Matrix(0, 1, -1, 0, 0, 0);
//Matrix CCW = new Matrix(0, -1, 1, 0, 0, 0);

//foreach (FloorTile cmd in m_host.Status.path)
//{
//    switch (cmd.direction)
//    {
//    case MoveCommand.Direction.Forward:
//        Point move = new Point((int)((double)(currentDir[0].X * cmd.distance) / m_ratioX), (int)((double)(currentDir[0].Y * cmd.distance) / m_ratioY));
//        lineEnd.Offset(move);
//        break;
//    case MoveCommand.Direction.CCW:
//        s.DrawLine(new Pen(new SolidBrush(Color.Blue)), lineStart, lineEnd);
//        lineStart = new Point(lineEnd.X, lineEnd.Y);
//        CCW.TransformPoints(currentDir);
//        break;
//    case MoveCommand.Direction.CW:
//        s.DrawLine(new Pen(new SolidBrush(Color.Blue)), lineStart, lineEnd);
//        lineStart = new Point(lineEnd.X, lineEnd.Y);
//        CW.TransformPoints(currentDir);
//        break;
//    }  
//}