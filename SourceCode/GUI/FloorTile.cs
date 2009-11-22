using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace See3PO
{
    public class FloorTile
    {
        bool m_walkable;
        private Point m_location;
        private FloorPlan m_floorPlan;
        private bool m_endPoint;

        public FloorTile(int x, int y, bool walkable)
        {
            m_location.X = x;
            m_location.Y = y;
            m_walkable = walkable;
            m_endPoint = false;
        }

        public override String ToString()
        {
            if (this.m_walkable)
                return "0";
            else
                return "1";
        }

        public Color toPixel() {
            if (m_walkable)
                return Color.White;
            else 
                return Color.Black;
        }

        public bool endPoint
        {
            get { return m_endPoint; }
            set { m_endPoint = value; }
        }
    }
}