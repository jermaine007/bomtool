using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace NooneUI.Framework
{
    public abstract class LightUserControlBase : UserControl, IContainerProvider, ILoggerProvider, IView
    {

        public virtual string Id => this.GetType().FullName;

        protected readonly ILogger logger;

        protected LightUserControlBase() => logger = (this as ILoggerProvider).Logger;
    }
}