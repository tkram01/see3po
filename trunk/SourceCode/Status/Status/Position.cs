using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Status
{
    class Position
    {
        public Point position;
        private int _facing;
        public int facing 
        { 
            get {return facing;}
            set { facing = value % 360; }
        }
    }
}
