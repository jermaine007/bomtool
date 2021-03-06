using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Styling;
using Avalonia.VisualTree;
using Noone.UI.Core;
using Noone.UI.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace Noone.UI.Controls
{
    [AutoRegister]
    public abstract class LightWindowBase : Window,
        IContainerProvider,
        ILoggerProvider,
        IStyleable,
        IView
    {
        private const string DEBUG = "DEBUG";

        protected Button minimizeButton;
        protected Button maximizeButton;
        protected Button restoreButton;
        protected Button closeButton;

        protected readonly ILogger logger;
        protected readonly IContainer container;

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
            logger = ((ILoggerProvider)this).Logger;
            container = ((IContainerProvider)this).Container;

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //this.HasSystemDecorations = false;
            // this.ExtendClientAreaToDecorationsHint = false;
            // this.ExtendClientAreaChromeHints = Avalonia.Platform.ExtendClientAreaChromeHints.SystemChrome;
            this.SystemDecorations = SystemDecorations.None;

            EnableDevelopTools();

            logger.Debug($"View -> {((IView)this).Id} has been created.");
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);
            if (!this.IsFixedPosition)
            {
                this.BeginMoveDrag(e);
            }
        }

        // https://github.com/AvaloniaUI/Avalonia/pull/3462
        // Move into seprated package Avalonia.Diagnostics
        [Conditional(DEBUG)]
        protected void EnableDevelopTools() => this.AttachDevTools();

        protected virtual void HandleSystemButtons()
        {
            logger.Debug("Enter handle system button visibility");
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

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
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
