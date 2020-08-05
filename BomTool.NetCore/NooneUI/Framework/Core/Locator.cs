using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace NooneUI.Framework
{
    public abstract class Locator : ILoggerProvider, IContainerProvider
    {

        public static readonly AttachedProperty<bool> AutoWiredProperty =
            AvaloniaProperty.RegisterAttached<Locator, StyledElement, bool>("AutoWired", false);

        public IContainer Container { get; }

        public ILogger Logger { get; }

        public static bool GetAutoWired(StyledElement element) => element.GetValue(AutoWiredProperty);

        public static void SetAutoWired(StyledElement element, bool value) => element.SetValue(AutoWiredProperty, value);

        protected Locator()
        {
            Logger = ((ILoggerProvider)this).Logger.Configure(this);
            Container = ((IContainerProvider)this).Container;

            AutoWiredProperty.Changed.AddClassHandler<StyledElement>((o, e) =>
            {
                if ((bool)e.NewValue)
                {
                    Container.Get<IMvvmRelationships>().GetViewModel((IView)o);
                }
            });
        }
    }
}