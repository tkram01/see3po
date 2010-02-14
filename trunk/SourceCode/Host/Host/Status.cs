using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

using FloorPlanAndTile;
using FloorTile = FloorPlanAndTile.FloorTile;
using FloorPlan = FloorPlanAndTile.FloorPlan;

namespace Host
{
    /// <summary>
    /// This central structure will hold all relevant data
    /// </summary>
    public class Status
    {
        /// <summary>
        /// The max number of images to store in the Stack
        /// </summary>
        private const int IMG_CAP = 10; 
       
        /// <summary>
        /// An image pair for the Locator
        /// </summary>
        private Image[] m_Images;

        /// <summary>
        /// A representation of the robot's current local environment
        /// </summary>
        private FloorPlan m_FloorPlan;

        /// <summary>
        /// A list of FloorTile objects to be traversed by the robot
        /// </summary>
        private List<FloorTile> m_Moves;

        /// <summary>
        /// The robot's current location and facing
        /// </summary>
        private Position m_Position;

        /// <summary>
        /// The robot's destination
        /// </summary>
        private Point m_EndPoint;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="floorPlan">An image of the floorplan to be constructed</param>
        /// <param name="scale">The scale of the image provided in pixels per foot</param>
        public Status(Image floorplan, double scale){
            m_FloorPlan = new FloorPlan(floorplan, scale);

            m_Moves = new List<FloorTile>();
            m_Images = null;
            m_Moves = new List<FloorTile>();
            m_Position = new Position(new Point(0, 0), 0);
        }

        /// <summary>
        /// gets the most recent image, or adds a more recent image to the stack
        /// </summary>
        public Image[] Images
        {
            get { return m_Images; }
            set { Images = value; }
        }

        /// <summary>
        /// The current path
        /// </summary>
        public List<FloorTile> path
        {
            get { return m_Moves; }
            set { m_Moves = value; }
        }

        public Position position
        {
            get { return m_Position; }
            set 
            {
                m_Position = value;
                m_FloorPlan.setStartTile(position.location.X, position.location.Y);
            }
        }

        public Point endPoint
        {
            get { return m_EndPoint; }
            set 
            { 
                m_EndPoint = value;
                m_FloorPlan.setTargetTile(endPoint.X, endPoint.Y);
            }
        }

        public FloorPlan floorPlan {
            get { return m_FloorPlan; }
        }
    }
}
