using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace See3PO
{
    public class FloorTile
    {
        private bool m_walkable;
        readonly private Point m_location;
        private FloorPlan m_floorPlan;
        private bool m_endPoint;
        private List<FloorTile> m_neighbours;
        private bool m_start;
        private bool m_target;

        public FloorTile()
        {
        }

        public void SetStart(bool start)
        {
            m_start = start;
        }
        public void SetTarget(bool target)
        {
            m_target = target;
        }
        public FloorTile(int x, int y, bool walkable,FloorPlan floorPlan)
        {
            m_location.X = x;
            m_location.Y = y;
            m_walkable = walkable;
            m_endPoint = false;
            m_floorPlan = floorPlan;
            m_neighbours = new List<FloorTile>();
            m_start = false;
            m_target = false;
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

        public Point Position
        {
            get {return m_location; }
        }
        public bool Iswalkable()
        {
            return m_walkable;
        }

        public bool IsStart()
        {
            return m_start;
        }
        public bool IsTarget()
        {
            return m_target;
        }
        public bool endPoint
        {
            get { return m_endPoint; }
            set { m_endPoint = value; }
        }

        public List<FloorTile> getNeighbours()
        {
            return   m_neighbours;
        }

        public void ResetWalkableNeighbors()
        {
          
           FloorTile tile1 = m_floorPlan.getTile(this.m_location.X - 1, this.m_location.Y);
           if ((tile1 != null)&&(tile1.Iswalkable()))
               m_neighbours.Add(tile1);
           tile1 = m_floorPlan.getTile(this.m_location.X + 1, this.m_location.Y);
           if ((tile1 != null) && (tile1.Iswalkable()))
               m_neighbours.Add(tile1);
           tile1= m_floorPlan.getTile(this.m_location.X , this.m_location.Y-1);
           if ((tile1 != null) && (tile1.Iswalkable()))
               m_neighbours.Add(tile1);
           tile1= m_floorPlan.getTile(this.m_location.X, this.m_location.Y+1);
           if ((tile1 != null) && (tile1.Iswalkable()))
               m_neighbours.Add(tile1);
         
        }
    }
}