using NooneUI.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI.Logging
{
    [AutoRegisterType("NooneUI.Trace")]
    public class Trace
    {
        private static readonly string Sender = "QML =>";

        public ILogger Logger => ServicesContainer.Instance.Get<ILogger>();

        public void Info(string message) => Logger.Info($"{Sender} {message}");

        public void Debug(string message) => Logger.Info($"{Sender} {message}");

        public void Warn(string message) => Logger.Info($"{Sender} {message}");

        public void Error(string message) => Logger.Info($"{Sender} {message}");

        public void Fatal(string message) => Logger.Info($"{Sender} {message}");
    }
}
