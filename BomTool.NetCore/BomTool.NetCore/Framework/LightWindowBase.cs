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

namespace BomTool.NetCore.Framework
{
    public class LightWindowBase : Window, IContainerProvider, ILoggerProvider, IStyleable
    {
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

        Type IStyleable.StyleKey => typeof(LightWindowBase);

        protected readonly ILogger logger;

        protected LightWindowBase()
        {
            this.HasSystemDecorations = false;
            this.PointerPressed += OnPointerPressed;
            this.logger = (this as ILoggerProvider).Logger;
            SystemButtonsProperty.Changed.AddClassHandler<LightWindowBase>((o, e) => this.HandleSystemButtons());
            WindowStateProperty.Changed.AddClassHandler<LightWindowBase>((o, e) => this.HandleSystemButtons());
            EnableDevelopTools();
        }

        protected void OnPointerPressed(object sender, PointerPressedEventArgs e) => this.BeginMoveDrag(e);

        [Conditional("DEBUG")]
        protected void EnableDevelopTools() => this.AttachDevTools();

        private void HandleSystemButtons()
        {
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
            HandleSystemButtons();

        }
    }
}