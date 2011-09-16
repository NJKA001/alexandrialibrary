using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

using log4net;

namespace Gnosis.Alexandria.Loggers
{
    public class Log4NetLogger
        : ILogger
    {
        public Log4NetLogger(ILog log)
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
