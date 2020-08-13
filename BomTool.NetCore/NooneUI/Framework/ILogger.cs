using System;

namespace NooneUI.Framework
{
    /// <summary>
    /// Inteface provides to log.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Initialize a logger name.
        /// </summary>
        /// <param name="o"></param>
        void Initialize(object o);

        /// <summary>
        /// Info
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Info(string message, params object[] args);

        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Debug(string message, params object[] args);

        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Warn(string message, params object[] args);

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Error(string message, params object[] args);

        /// <summary>
        /// Error with exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="args"></param>
        void Error(string message, Exception ex, params object[] args);

        /// <summary>
        /// Fata error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Fatal(string message, params object[] args);

        /// <summary>
        /// Fatal error with exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="args"></param>
        void Fatal(string message, Exception ex, params object[] args);
    }
}
