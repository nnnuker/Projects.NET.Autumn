using System.Diagnostics;
using NLog;
using ILogger = MyServiceLibrary.Interfaces.Infrastructure.ILogger;

namespace MyServiceLibrary.Infrastructure.Loggers
{
    public class NlogLogger : ILogger
    {
        private readonly bool enabled;
        private readonly Logger logger;

        public NlogLogger()
        {
            enabled = new BooleanSwitch("logEnabled", "Logger switch").Enabled;
            logger = LogManager.GetCurrentClassLogger();
        }

        public void Trace(string message)
        {
            if (enabled)
            {
                logger.Trace(message);
            }
        }

        public void Debug(string message)
        {
            if (enabled)
            {
                logger.Debug(message);
            }
        }

        public void Info(string message)
        {
            if (enabled)
            {
                logger.Info(message);
            }
        }

        public void Warn(string message)
        {
            if (enabled)
            {
                logger.Warn(message);
            }
        }

        public void Error(string message)
        {
            if (enabled)
            {
                logger.Error(message);
            }
        }

        public void Fatal(string message)
        {
            if (enabled)
            {
                logger.Fatal(message);
            }
        }
    }
}