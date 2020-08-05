using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace NooneUI.Framework
{
    public abstract class LightUserControlBase : UserControl, ILoggerProvider, IContainerProvider, IView
    {
        public ILogger Logger { get; }
        public IContainer Container { get; }

        protected LightUserControlBase()
        {
            Logger = ((ILoggerProvider)this).Logger.Configure(this);
            Container = ((IContainerProvider)this).Container;
            Logger.Debug($"View -> {((IView)this).Id} has been created.");
        }
    }
}