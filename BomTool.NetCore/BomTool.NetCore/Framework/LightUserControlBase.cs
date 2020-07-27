using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BomTool.NetCore.Framework
{
    public class UserControlBase : UserControl, IContainerProvider, ILoggerProvider
    {
        protected readonly ILogger logger;

        protected UserControlBase() => logger = (this as ILoggerProvider).Logger;
    }
}