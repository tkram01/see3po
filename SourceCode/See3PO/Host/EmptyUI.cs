using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace See3PO
{
    /// <summary>
    /// An empty UI class for testing
    /// </summary>
    public class EmptyUI : UI
    {
        /// <summary>
        /// This allows messages to be displayed to any UI
        /// </summary>
        /// <param name="msg"> Any message that should be displayed to the user</param>
        public void PostMessage(String msg) { }

        /// <summary>
        /// Passes information about the status of the connection between Host and Remote
        /// </summary>
        /// <param name="status">the connection status</param>
        public void PostConnection(String status) { }

        /// <summary>
        /// Passes information about the status of the connection between Host and Remote
        /// </summary>
        /// <param name="status">the connection status</param>
        public void PostImage(System.Drawing.Image image) { }

        public void updateUI() { }
    }
}
