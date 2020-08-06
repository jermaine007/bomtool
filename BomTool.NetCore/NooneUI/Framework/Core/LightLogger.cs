using System;
using NLog;

namespace NooneUI.Framework
{
    internal class LightLogger : ILogger
    {
        public static volatile bool GlobalEnableLogging = true;

        private Logger nullLogger;
        private Logger logger;

        protected Logger Logger => GlobalEnableLogging ? (logger ??= LogManager.GetLogger(nameof(LightLogger))) : (nullLogger ??= LogManager.CreateNullLogger());

        public void Debug(string message, params object[] args) => Logger.Debug(message, args);

        public void Error(string message, params object[] args) => Logger.Error(message, args);

        public void Error(string message, Exception ex, params object[] args) => Logger.Error(ex, message, args);

        public void Info(string message, params object[] args) => Logger.Info(message, args);

        public void Warn(string message, params object[] args) => Logger.Warn(message, args);

        public void Fatal(string message, params object[] args) => Logger.Fatal(message, args);

        public void Fatal(string message, Exception ex, params object[] args) => Logger.Fatal(ex, message, args);

        public void Initialize(object o) => logger = LogManager.GetLogger(o.GetType().FullName);

        public void EnableLogging(bool value) => GlobalEnableLogging = value;

    }
}
