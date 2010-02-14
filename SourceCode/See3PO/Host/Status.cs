using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

using FloorPlanAndTile;
using FloorTile = FloorPlanAndTile.FloorTile;
using FloorPlan = FloorPlanAndTile.FloorPlan;

namespace See3PO
{
    /// <summary>
    /// This central structure will hold all relevant data
    /// </summary>
    public class Status
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="floorPlan">An image of the floorplan to be constructed</param>
        /// <param name="scale">The scale of the image provided in pixels per foot</param>
        public Status(Image floorplan, double scale)
        {
            m_FloorPlan = new FloorPlan(floorplan, scale);

            m_Moves = new List<FloorTile>();
            m_Images = null;
            m_Moves = new List<FloorTile>();
            m_Position = new Position(new Point(0, 0), 0);
        }


        //************************************************************************************************
        //      private attributes
        //************************************************************************************************


        private const int IMG_CAP = 10;         // The max number of images to store in the Stack

        private Image[] m_Images;               // An image pair for the Locator

        private FloorPlan m_FloorPlan;          // A representation of the robot's current local environment

        private List<FloorTile> m_Moves;        // A list of FloorTile objects to be traversed by the robot

        private Position m_Position;            // The robot's current location and facing

        private Point m_EndPoint;               // The robot's destination

        //************************************************************************************************
        //       Getters and Setters for private attributes
        //************************************************************************************************

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
        public List<FloorTile> Path
        {
            get { return m_Moves; }
            set { m_Moves = value; }
        }

        /// <summary>
        /// The Robot's current positiion
        /// </summary>
        public Position Position
        {
            get { return m_Position; }
            set
            {
                m_Position = value;
                m_FloorPlan.setStartTile(m_Position.location.X, m_Position.location.Y);
            }
        }

        /// <summary>
        /// The Robot's Current Destination / Endpoint
        /// </summary>
        public Point EndPoint
        {
            get { return m_EndPoint; }
            set
            {
                m_EndPoint = value;
                m_FloorPlan.setTargetTile(m_EndPoint.X, m_EndPoint.Y);
            }
        }

        /// <summary>
        /// The FloorPlan
        /// </summary>
        public FloorPlan FloorPlan
        {
            get { return m_FloorPlan; }
        }
    }
}
