using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FloorPlanAndTile;

namespace See3PO
{
    interface PathFinder
    {
        List<FloorPlanAndTile.FloorTile> getPath();
    }
}
