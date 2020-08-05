using System;
using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Styling;
using Avalonia.VisualTree;

namespace NooneUI.Framework
{
    public abstract class LightWindowBase : Window, ILoggerProvider, IContainerProvider, IStyleable, IView
    {
        private const string DEBUG = "DEBUG";

        private Button minimizeButton;
        private Button maximizeButton;
        private Button restoreButton;
        private Button closeButton;

        public static readonly StyledProperty<IBitmap> LogoProperty =
            AvaloniaProperty.Register<LightWindowBase, IBitmap>(nameof(Logo));

        public static readonly StyledProperty<object> MenuBarProperty =
             AvaloniaProperty.Register<LightWindowBase, object>(nameof(MenuBar));

        public static readonly StyledProperty<object> StatusBarProperty =
             AvaloniaProperty.Register<LightWindowBase, object>(nameof(StatusBar));

        public static readonly StyledProperty<SystemButtons> SystemButtonsProperty =
             AvaloniaProperty.Register<LightWindowBase, SystemButtons>(nameof(SystemButtons), SystemButtons.All);


        public static readonly StyledProperty<bool> IsFixedPositionProperty =
             AvaloniaProperty.Register<LightWindowBase, bool>(nameof(IsFixedPosition), false);

        public ILogger Logger { get; }
        public IContainer Container { get; }

        public IBitmap Logo
        {
            get { return GetValue(LogoProperty); }
            set { SetValue(LogoProperty, value); }
        }

        public object MenuBar
        {
            get { return GetValue(MenuBarProperty); }
            set { SetValue(MenuBarProperty, value); }
        }

        public object StatusBar
        {
            get { return GetValue(StatusBarProperty); }
            set { SetValue(StatusBarProperty, value); }
        }

        public SystemButtons SystemButtons
        {
            get { return GetValue(SystemButtonsProperty); }
            set { SetValue(SystemButtonsProperty, value); }
        }

        public bool IsFixedPosition
        {
            get { return GetValue(IsFixedPositionProperty); }
            set { SetValue(IsFixedPositionProperty, value); }
        }

        Type IStyleable.StyleKey => typeof(LightWindowBase);

        protected LightWindowBase()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.HasSystemDecorations = false;
            Logger = ((ILoggerProvider)this).Logger.Configure(this);
            Container = ((IContainerProvider)this).Container;
            EnableDevelopTools();

            Logger.Debug($"View -> {((IView)this).Id} has been created.");
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);
            if (!this.IsFixedPosition)
            {
                this.BeginMoveDrag(e);
            }
        }

        [Conditional(DEBUG)]
        protected void EnableDevelopTools() => this.AttachDevTools();

        private void HandleSystemButtons()
        {
            Logger.Debug("Enter handle system button visibility");
            minimizeButton.IsVisible = this.SystemButtons != SystemButtons.None
                && this.SystemButtons.HasFlag(SystemButtons.Minimize);

            maximizeButton.IsVisible = this.SystemButtons != SystemButtons.None
                && this.SystemButtons.HasFlag(SystemButtons.Maximize)
                && (this.WindowState == WindowState.Normal
                    || this.WindowState == WindowState.Minimized);

            restoreButton.IsVisible = this.SystemButtons != SystemButtons.None
                && this.SystemButtons.HasFlag(SystemButtons.Maximize)
                && this.WindowState == WindowState.Maximized;

            closeButton.IsVisible = this.SystemButtons != SystemButtons.None
                && this.SystemButtons.HasFlag(SystemButtons.Close);
        }

        protected override void OnTemplateApplied(Avalonia.Controls.Primitives.TemplateAppliedEventArgs e)
        {
            var buttons = this.GetVisualDescendants().OfType<Button>().ToList();
            minimizeButton = buttons.FirstOrDefault(x => x.Name == "MinimizeButton");
            maximizeButton = buttons.FirstOrDefault(x => x.Name == "MaximizeButton");
            restoreButton = buttons.FirstOrDefault(x => x.Name == "RestoreButton");
            closeButton = buttons.FirstOrDefault(x => x.Name == "CloseButton");
            SystemButtonsProperty.Changed.AddClassHandler<LightWindowBase>((o, e) => this.HandleSystemButtons());
            WindowStateProperty.Changed.AddClassHandler<LightWindowBase>((o, e) => this.HandleSystemButtons());
            HandleSystemButtons();
        }
    }
}