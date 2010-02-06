using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Host;

namespace Host
{
    interface PathFinder
    {
        List<FloorTile> getPath();
    }
}
