using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;

namespace Gnosis.Alexandria.Logging
{
    public class Log4NetLogger
        : ILogger
    {
        public Log4NetLogger(string name)
        {
            log4net.Config.XmlConfigurator.Configure();
            log = LogManager.GetLogger(name);
        }

        public Log4NetLogger(Type type)
        {
            log4net.Config.XmlConfigurator.Configure();
            log = LogManager.GetLogger(type);
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
