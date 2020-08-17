using Avalonia;

namespace Noone.UI.Core
{
    /// <summary>
    /// A class that provides attached property <see cref="Locator.AutoWiredProperty"/> to let a view auto wired a DataContext.
    /// </summary>
    public abstract class Locator : ILoggerProvider, IContainerProvider
    {

        public static readonly AttachedProperty<bool> AutoWiredProperty =
            AvaloniaProperty.RegisterAttached<Locator, AvaloniaObject, bool>("AutoWired", false);

        protected readonly IContainer container;

        protected readonly ILogger logger;

        public static bool GetAutoWired(AvaloniaObject element) => element.GetValue(AutoWiredProperty);

        public static void SetAutoWired(AvaloniaObject element, bool value) => element.SetValue(AutoWiredProperty, value);

        protected Locator()
        {
            logger = ((ILoggerProvider)this).Logger;
            container = ((IContainerProvider)this).Container;

            AutoWiredProperty.Changed.AddClassHandler<AvaloniaObject>((o, e) =>
            {
                if ((bool)e.NewValue)
                {
                    IView view = o as IView;
                    IViewModel vm = container.Get<IMvvmRelationships>().GetViewModel(view);
                    container.Get<IViewPresenter>().AddOrUpdateStore(vm, view);
                }
            });
        }
    }
}
