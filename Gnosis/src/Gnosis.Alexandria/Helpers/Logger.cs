using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;

using Gnosis.Core;

namespace Gnosis.Alexandria.Helpers
{
    public class Logger
        : ILogger
    {
        public Logger(ILog log)
        {
            this.log = log;
        }

        private readonly ILog log;

        public void Debug(string message)
        {
            log.Debug(message);
        }

        public void Info(string message)
        {
            log.Info(message);
        }

        public void Error(string message)
        {
            log.Error(message);
        }

        public void Error(string message, Exception ex)
        {
            log.Error(message, ex);
        }

        public void Warn(string message)
        {
            log.Warn(message);
        }
    }
}
