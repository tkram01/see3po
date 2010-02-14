using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Host;
using FloorPlanAndTile;

namespace Host
{
    interface PathFinder
    {
        List<FloorPlanAndTile.FloorTile> getPath();
    }
}
