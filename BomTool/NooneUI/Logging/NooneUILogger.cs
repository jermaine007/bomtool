using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI.Logging
{
    internal class NooneUILogger : ILogger
    {
        private Logger nullLogger;
        private Logger logger;
        private bool enableLogging;

        protected Logger Logger => enableLogging ?
            logger ?? (logger = LogManager.GetLogger("NooneUI")) : nullLogger ?? (nullLogger = LogManager.CreateNullLogger());

        void ILogger.Debug(string message, params object[] args)
        {
            Logger.Debug(message, args);
        }

        void ILogger.Error(string message, params object[] args)
        {
            Logger.Error(message, args);
        }

        void ILogger.Error(string message, Exception ex, params object[] args)
        {
            Logger.Error(ex, message, args);
        }

        void ILogger.Info(string message, params object[] args)
        {
            Logger.Info(message, args);
        }

        void ILogger.Warn(string message, params object[] args)
        {
            Logger.Warn(message, args);
        }

        void ILogger.Fatal(string message, params object[] args)
        {
            Logger.Fatal(message, args);
        }

        void ILogger.Fatal(string message, Exception ex, params object[] args)
        {
            Logger.Fatal(ex, message, args);
        }

        bool ILogger.EnableLogging { get => enableLogging; set => enableLogging = value; }
    }
}
