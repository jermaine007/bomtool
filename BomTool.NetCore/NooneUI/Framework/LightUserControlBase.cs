using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace NooneUI.Framework
{
    public abstract class LightUserControlBase : UserControl, IContainerProvider, ILoggerProvider, IView
    {

        public virtual string Id => this.GetType().FullName;

        protected readonly ILogger logger;

        protected LightUserControlBase() => logger = (this as ILoggerProvider).Logger;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            // due to use ViewLocator, if DataContext is already be set, do not set again.
            if (this.DataContext != null)
            {
                return;
            }
            var type = Relationship.Current.Lookup(this.GetType());
            if (type == null)
            {
                return;
            }
            IViewModel vm = (this as IContainerProvider).Container.Get(type) as IViewModel;
            if (vm != null)
            {
                vm.View = this;
                this.DataContext = vm;
            }
        }
    }
}