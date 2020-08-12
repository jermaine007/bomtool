using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace NooneUI.Framework
{
    [AutoRegister]
    public abstract class LightUserControlBase : UserControl, IBaseServiceProvider, IView
    {
        protected readonly ILogger logger;
        protected readonly IContainer container;

        protected LightUserControlBase()
        {
            logger = ((IBaseServiceProvider)this).Logger;
            container = ((IBaseServiceProvider)this).Container;
            logger.Debug($"View -> {((IView)this).Id} has been created.");
        }
    }
}
