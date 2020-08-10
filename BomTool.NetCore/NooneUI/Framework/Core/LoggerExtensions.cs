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
        /// Disable looging
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static void Enable(bool enable) => LightLogger.EnableLogging(enable);
    }
}
