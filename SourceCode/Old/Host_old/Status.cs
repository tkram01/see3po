using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Host
{
    public class Status
    {
        private const int IMG_CAP = 10;

        private Stack<Image> m_Images;
        private FloorPlan m_FloorPlan;
        private List<FloorTile> m_Moves;
        private Position m_Position;
        private Point m_EndPoint;

        public Status(Image floorPlan, double scale){
            m_FloorPlan = new FloorPlan(floorPlan, scale);
            m_Moves = new List<FloorTile>();
            m_Images = new Stack<Image>(IMG_CAP);
            m_Moves = new List<FloorTile>();
            m_Position = new Position(new Point(0, 0), 0);
        }

        public Image nextImage
        {
            get { return m_Images.Pop(); }
            set { m_Images.Push(value); }
        }

        public void clearImages() {
            m_Images = new Stack<Image>(IMG_CAP);
        }

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
