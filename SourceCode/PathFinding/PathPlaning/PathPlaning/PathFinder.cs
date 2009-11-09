using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PathPlaning
{
    interface PathFinder
    {
        public ArrayList<FloorTile> getPath();
    }
}
