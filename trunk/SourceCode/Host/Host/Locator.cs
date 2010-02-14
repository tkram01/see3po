using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Host
{
    /// <summary>
    /// A Locator class will take images as input and return a 
    /// Position object representing see3PO's current location and facing
    /// </summary>
    public interface Locator
    {
        /// <summary>
        /// this method take images as input and return a 
        /// Position object representing see3PO's current location and facing
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        Position GetPosition(Image[] images);
    }
}
