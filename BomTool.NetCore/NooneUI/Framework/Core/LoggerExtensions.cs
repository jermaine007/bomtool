using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI.Framework
{
    public static class LoggerExtensions
    {
        /// <summary>
        /// Configure logger name
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static ILogger Configure(this ILogger logger, object o)
        {
            logger.Initialize(o);
            return logger;
        }

        /// <summary>
        /// Enable logging
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static ILogger Enable(this ILogger logger)
        {
            logger.EnableLogging(true);
            return logger;
        }

        /// <summary>
        /// Disable looging
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static ILogger Disable(this ILogger logger)
        {
            logger.EnableLogging(false);
            return logger;
        }

        /// <summary>
        /// Setup logger instance
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static ILogger Setup(this ILogger logger, Action<ILogger> action)
        {
            action(logger);
            return logger;
        }
    }
}
