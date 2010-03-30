/*
$license$
$Rev$
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace See3PO
{
    public class Position
    {
        public Point location;
        private int _facing;  // 0 = East, 90 = North, 180 = West, 270 = South
        public int facing 
        { 
            get {return _facing;}
            set { _facing = value % 360; }
        }

        public Position(Point location, int facing) {
            this.location = location;
            this._facing = facing;
        }

        public Position(int x, int y, int facing)
        {
            this.location = new Point(x, y);
            this._facing = facing;
        }
    }
}
