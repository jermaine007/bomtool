using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace NooneUI.Framework
{
    public abstract class Locator : IBaseServiceProvider
    {

        public static readonly AttachedProperty<bool> AutoWiredProperty =
            AvaloniaProperty.RegisterAttached<Locator, StyledElement, bool>("AutoWired", false);

        protected readonly IContainer container;

        protected readonly ILogger logger;

        public static bool GetAutoWired(StyledElement element) => element.GetValue(AutoWiredProperty);

        public static void SetAutoWired(StyledElement element, bool value) => element.SetValue(AutoWiredProperty, value);

        protected Locator()
        {
            logger = ((IBaseServiceProvider)this).Logger;
            container = ((IBaseServiceProvider)this).Container;

            AutoWiredProperty.Changed.AddClassHandler<StyledElement>((o, e) =>
            {
                if ((bool)e.NewValue)
                {
                    container.Get<IMvvmRelationships>().GetViewModel((IView)o);
                }
            });
        }
    }
}
