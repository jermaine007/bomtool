using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace NooneUI.Framework
{
    [AutoRegister]
    public abstract class LightUserControlBase : UserControl,
        IContainerProvider,
        ILoggerProvider,
        IView
    {
        protected readonly ILogger logger;
        protected readonly IContainer container;

        protected LightUserControlBase()
        {
            logger = ((ILoggerProvider)this).Logger;
            container = ((IContainerProvider)this).Container;
            logger.Debug($"View -> {((ICanSetup)this).Id} has been created.");
        }
    }
}
