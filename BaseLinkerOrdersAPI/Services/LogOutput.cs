using BaseLinkerOrdersAPI.Interfaces;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLinkerOrdersAPI.Services
{
  class LogOutput : IOutput
  {
    private ILog logger;

    public LogOutput Setup(string appName, string logFilePath)
    {

      var hierarchy = (Hierarchy)LogManager.GetRepository();

      var patternLayout = new PatternLayout
      {
        ConversionPattern = "%date [%thread] %-p %logger - %message%newline",
        Header = "********************************[START]********************************\r\n",
        Footer = "********************************[STOP]********************************"
      };
      patternLayout.ActivateOptions();

      var roller = new RollingFileAppender
      {
        AppendToFile = true,
        File = logFilePath != null && !string.IsNullOrWhiteSpace(logFilePath) ? logFilePath : @"Logs.txt",
        Layout = patternLayout,
        MaximumFileSize = "1GB"
      };
      roller.ActivateOptions();

      hierarchy.Root.AddAppender(roller);
      hierarchy.Root.Level = Level.Info;
      hierarchy.Configured = true;

      logger = LogManager.GetLogger(appName);
      return this;
    }

    public void WriteLine(int level, string message)
    {
      string msg = message;
      LogMessage(level, msg);
    }

    public void WriteLine(int level, Exception ex, string message)
    {
      string msg = message;
      LogMessage(level, ex, msg);
    }

    private void LogMessage(int level, string message)
    {
      int loggerMinimumLevel = 0;
      if (loggerMinimumLevel <= level) {
        if (level == 0 )
        {
          logger.Debug(message);
        }
        else if (level == 1)
        {
          logger.Info(message);
        }
        else if (level == 2)
        {
          logger.Warn(message);
        }
        else if (level == 3)
        {
          logger.Error(message);
        }
        else if (level == 4)
        {
          logger.Fatal(message);
        }
      }
    }

    private void LogMessage(int level, Exception ex, string message)
    {
      int loggerMinimumLevel = 0;
      if (loggerMinimumLevel <= level)
      {
        if (level == 0)
        {
          logger.Debug(message, ex);
        }
        else if (level == 1)
        {
          logger.Info(message, ex);
        }
        else if (level == 2)
        {
          logger.Warn(message, ex);
        }
        else if (level == 3)
        {
          logger.Error(message, ex);
        }
        else if (level == 4)
        {
          logger.Fatal(message, ex);
        }
      }
    }
  }
}
