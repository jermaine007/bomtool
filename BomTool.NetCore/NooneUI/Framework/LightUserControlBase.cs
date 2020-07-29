using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace NooneUI.Framework
{
    public class LightUserControlBase : UserControl, IContainerProvider, ILoggerProvider
    {
        protected readonly ILogger logger;

        protected LightUserControlBase() => logger = (this as ILoggerProvider).Logger;
    }
}