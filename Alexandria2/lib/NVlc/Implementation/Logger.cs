//    nVLC
//    
//    Author:  Roman Ginzburg
//
//    nVLC is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    nVLC is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.
//     
// ========================================================================

using Declarations;

namespace Implementation
{
   internal class Logger : ILogger
   {
      //NLog.Logger m_logImpl;
       private readonly log4net.ILog log;

      public Logger()
      {
          log4net.Config.XmlConfigurator.Configure();
          log = log4net.LogManager.GetLogger(typeof(Logger));
         //m_logImpl = NLog.LogManager.GetCurrentClassLogger();
      }

      #region ILogger Members

      public void Debug(string debug)
      {
          log.Debug(debug);
         //m_logImpl.Debug(debug);
      }

      public void Info(string info)
      {
          log.Info(info);
         //m_logImpl.Info(info);
      }

      public void Warning(string warn)
      {
          log.Warn(warn);
         //m_logImpl.Warn(warn);
      }

      public void Error(string error)
      {
          log.Error(error);
         //m_logImpl.Error(error);
      }

      #endregion
   }
}
