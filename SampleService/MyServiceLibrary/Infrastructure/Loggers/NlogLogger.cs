using System;
using System.Diagnostics;
using NLog;
using ILogger = MyServiceLibrary.Interfaces.Infrastructure.ILogger;

namespace MyServiceLibrary.Infrastructure.Loggers
{
    public class NlogLogger : ILogger
    {
        private readonly bool enabled;
        private readonly Logger logger;

        public NlogLogger(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException($"{nameof(name)} argument is null or empty string");
            }

            this.enabled = new BooleanSwitch("logEnabled", "Logger switch").Enabled;
            this.logger = LogManager.GetLogger(name);
        }

        public void Trace(string message)
        {
            if (this.enabled)
            {
                this.logger.Trace(message);
            }
        }

        public void Debug(string message)
        {
            if (this.enabled)
            {
                this.logger.Debug(message);
            }
        }

        public void Info(string message)
        {
            if (this.enabled)
            {
                this.logger.Info(message);
            }
        }

        public void Warn(string message)
        {
            if (this.enabled)
            {
                this.logger.Warn(message);
            }
        }

        public void Error(string message)
        {
            if (this.enabled)
            {
                this.logger.Error(message);
            }
        }

        public void Fatal(string message)
        {
            if (this.enabled)
            {
                this.logger.Fatal(message);
            }
        }
    }
}