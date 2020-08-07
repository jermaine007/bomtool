using Avalonia;

namespace NooneUI.Framework
{
    /// <summary>
    ///
    /// </summary>
    public abstract class Locator : IBaseServiceProvider
    {

        public static readonly AttachedProperty<bool> AutoWiredProperty =
            AvaloniaProperty.RegisterAttached<Locator, AvaloniaObject, bool>("AutoWired", false);

        protected readonly IContainer container;

        protected readonly ILogger logger;

        public static bool GetAutoWired(AvaloniaObject element) => element.GetValue(AutoWiredProperty);

        public static void SetAutoWired(AvaloniaObject element, bool value) => element.SetValue(AutoWiredProperty, value);

        protected Locator()
        {
            logger = ((IBaseServiceProvider)this).Logger;
            container = ((IBaseServiceProvider)this).Container;

            AutoWiredProperty.Changed.AddClassHandler<AvaloniaObject>((o, e) =>
            {
                if ((bool)e.NewValue)
                {
                    container.Get<IMvvmRelationships>().GetViewModel((IView)o);
                }
            });
        }
    }
}
