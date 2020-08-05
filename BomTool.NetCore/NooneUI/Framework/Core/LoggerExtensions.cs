using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI.Framework
{
    public static class LoggerExtensions
    {
        public static ILogger Configure(this ILogger logger, object o)
        {
            logger.Initialize(o);
            return logger;
        }

        public static ILogger Enable(this ILogger logger)
        {
            logger.EnableLogging(true);
            return logger;
        }

        public static ILogger Disable(this ILogger logger)
        {
            logger.EnableLogging(false);
            return logger;
        }

        public static ILogger Setup(this ILogger logger, Action<ILogger> action)
        {
            action(logger);
            return logger;
        }
    }
}
