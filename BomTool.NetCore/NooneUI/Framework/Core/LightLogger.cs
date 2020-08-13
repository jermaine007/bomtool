using System;
using NLog;

namespace NooneUI.Framework
{
    internal class LightLogger : ILogger
    {
        /// <summary>
        /// Indicates that could be logged or not.
        /// </summary>
        public static volatile bool GlobalEnableLogging = true;

        private Logger nullLogger;
        private Logger logger;

        protected Logger Logger => GlobalEnableLogging ? (logger ??= LogManager.GetLogger(nameof(LightLogger))) : (nullLogger ??= LogManager.CreateNullLogger());

        /// <summary>
        /// <inheritdoc cref="ILogger.Debug(string, object[])"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Debug(string message, params object[] args) => Logger.Debug(message, args);

        /// <summary>
        /// <inheritdoc cref="ILogger.Error(string, object[])"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Error(string message, params object[] args) => Logger.Error(message, args);

        /// <summary>
        /// <inheritdoc cref="ILogger.Error(string, Exception, object[])"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="args"></param>
        public void Error(string message, Exception ex, params object[] args) => Logger.Error(ex, message, args);

        /// <summary>
        /// <inheritdoc cref="ILogger.Info(string, object[])"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Info(string message, params object[] args) => Logger.Info(message, args);

        /// <summary>
        /// <inheritdoc cref="ILogger.Warn(string, object[])"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Warn(string message, params object[] args) => Logger.Warn(message, args);

        /// <summary>
        /// <inheritdoc cref="ILogger.Fatal(string, object[])"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Fatal(string message, params object[] args) => Logger.Fatal(message, args);

        /// <summary>
        /// <inheritdoc cref="ILogger.Fatal(string, Exception, object[])"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="args"></param>
        public void Fatal(string message, Exception ex, params object[] args) => Logger.Fatal(ex, message, args);

        /// <summary>
        /// <inheritdoc cref="ILogger.Initialize(object)"/>
        /// </summary>
        /// <param name="o"></param>
        public void Initialize(object o) => logger = LogManager.GetLogger((o is Type) ? o.ToString() : o.GetType().FullName);

        /// <summary>
        /// Enable logging or disalbe logging
        /// </summary>
        /// <param name="value"></param>
        public static void EnableLogging(bool value) => GlobalEnableLogging = value;

    }
}
