using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Utilities
{
    public interface ILogger
    {
        void Debug(string message);
        void Info(string message);
        void Error(string message);
        void Error(string message, Exception ex);
        void Warn(string message);
    }
}
