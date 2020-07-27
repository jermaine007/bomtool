using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BomTool.NetCore.Framework
{
    public class LightWindowBase : Window, IContainerProvider, ILoggerProvider
    {
        protected readonly ILogger logger;

        protected LightWindowBase()
        {
            logger = (this as ILoggerProvider).Logger;
            EnableDevelopTools();
        }

        [Conditional("DEBUG")]
        protected void EnableDevelopTools() => this.AttachDevTools();

    }
}