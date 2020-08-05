using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace NooneUI.Framework
{
    public abstract class LightUserControlBase : UserControl, IContainerProvider, ILoggerProvider, IView
    {
        protected readonly ILogger logger;
        protected readonly LightContainer container;

        protected LightUserControlBase()
        {
            logger = ((ILoggerProvider)this).Logger.Configure(this);
            container = ((IContainerProvider)this).Container;
            logger.Debug($"View -> {((IView)this).Id} has been created.");
        }
    }
}