using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace See3PO
{
    public class FloorTile
    {
        int m_value;
        private Point m_location;
        private FloorPlan m_floorPlan;
        private bool m_endPoint;

        public FloorTile(int x, int y, int value)
        {
            m_location.X = x;
            m_location.Y = y;
            m_value = value;
            m_endPoint = false;
        }

        public override String ToString()
        {
            return "" + this.m_value;
        }

        public Color toPixel() {
            if (m_value == 0)
                return Color.Black;
            else 
                return Color.White;
        }

        public bool endPoint
        {
            get { return m_endPoint; }
            set { m_endPoint = value; }
        }
    }
}