using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Styling;

namespace BomTool.NetCore.Framework
{
    public class LightWindowBase : Window, IContainerProvider, ILoggerProvider, IStyleable
    {

         public static readonly StyledProperty<IBitmap> LogoProperty =
            AvaloniaProperty.Register<LightWindowBase, IBitmap>(nameof(Logo));
        
        public IBitmap Logo
        {
            get { return GetValue(LogoProperty); }
            set { SetValue(LogoProperty, value); }
        }

        Type IStyleable.StyleKey => typeof(LightWindowBase);

        protected readonly ILogger logger;

        protected LightWindowBase()
        {
            this.HasSystemDecorations = false;
            this.PointerPressed += OnPointerPressed;
            logger = (this as ILoggerProvider).Logger;
            EnableDevelopTools();
        }

        protected void OnPointerPressed(object sender, PointerPressedEventArgs e) => this.BeginMoveDrag(e);

        [Conditional("DEBUG")]
        protected void EnableDevelopTools() => this.AttachDevTools();

    }
}