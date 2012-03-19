using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Utilities
{
    public class DebugLogger
        : ILogger
    {
        public void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine("DEBUG: " + message);
        }

        public void Info(string message)
        {
            System.Diagnostics.Debug.WriteLine("INFO: " + message);
        }

        public void Error(string message)
        {
            System.Diagnostics.Debug.WriteLine("ERROR: " + message);
        }

        public void Error(string message, Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("ERROR: " + message);
            System.Diagnostics.Debug.WriteLine("       " + ex.Message);
            System.Diagnostics.Debug.WriteLine("       " + ex.StackTrace);
        }

        public void Warn(string message)
        {
            System.Diagnostics.Debug.WriteLine("WARN: " + message);
        }
    }
}
