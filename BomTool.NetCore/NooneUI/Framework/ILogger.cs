using System;

namespace NooneUI.Framework
{
    public interface ILogger
    {
        void Info(string message, params object[] args);

        void Debug(string message, params object[] args);

        void Warn(string message, params object[] args);

        void Error(string message, params object[] args);

        void Error(string message, Exception ex, params object[] args);

        void Fatal(string message, params object[] args);

        void Fatal(string message, Exception ex, params object[] args);

        bool EnableLogging { get; set; }
    }
}