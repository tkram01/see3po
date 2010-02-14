using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace See3PO
{
    /// <summary>
    /// A User Interface for Host
    /// </summary>
    public interface UI
    {
        /// <summary>
        /// This allows messages to be displayed to any UI
        /// </summary>
        /// <param name="msg"> Any message that should be displayed to the user</param>
        void PostMessage(String msg);

        /// <summary>
        /// Passes information about the status of the connection between Host and Remote
        /// </summary>
        /// <param name="status">the connection status</param>
        void PostConnection(String status);

        Locator getDefaultLocator();
    }
}
