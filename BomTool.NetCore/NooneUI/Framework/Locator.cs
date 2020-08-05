using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace NooneUI.Framework
{
    public abstract class Locator : IContainerProvider
    {
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
            AutoWiredProperty.Changed.AddClassHandler<StyledElement>((o, e) =>
            {
                if ((bool)e.NewValue)
                {
                    Relationships.Current.GetViewModel((IView)o);
                }
            });
        }
    }
}