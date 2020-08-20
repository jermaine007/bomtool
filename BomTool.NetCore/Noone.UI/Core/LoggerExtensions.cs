namespace Noone.UI.Core
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
        /// Enable or disable logging
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static ILogger Enable(this ILogger logger, bool enable)
        {
            LightLogger.EnableLogging(enable);
            return logger;
        }
    }
}
