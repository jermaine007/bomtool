using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace NooneUI.Framework
{
    public abstract class Locator : IContainerProvider, ILoggerProvider
    {
        protected LightContainer container;
        protected ILogger logger;

        public static readonly AttachedProperty<bool> AutoWiredProperty =
            AvaloniaProperty.RegisterAttached<Locator, StyledElement, bool>("AutoWired", false);

        public static bool GetAutoWired(StyledElement element)
        {
            return element.GetValue(AutoWiredProperty);
        }

        public static void SetAutoWired(StyledElement element, bool value)
        {
            element.SetValue(AutoWiredProperty, value);
        }

        protected Locator()
        {
            logger = ((ILoggerProvider)this).Logger.Configure(this);
            container = ((IContainerProvider)this).Container;

            AutoWiredProperty.Changed.AddClassHandler<StyledElement>((o, e) =>
            {
                if ((bool)e.NewValue)
                {
                    container.Get<MvvmRelationships>().GetViewModel((IView)o);
                }
            });
        }
    }
}