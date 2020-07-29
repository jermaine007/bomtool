using System.Reactive;
using Avalonia;
using Avalonia.Controls;
using ReactiveUI;

namespace NooneUI.Framework
{
    public class ApplicationCommands
    {
        public static ReactiveCommand<LightWindowBase, Unit> CloseWindow { get; private set; }
        public static ReactiveCommand<LightWindowBase, Unit> RestoreWindow { get; private set; }
        public static ReactiveCommand<LightWindowBase, Unit> MaximizeWindow { get; private set; }
        public static ReactiveCommand<LightWindowBase, Unit> MinimizeWindow { get; private set; }

        static ApplicationCommands()
        {
            CloseWindow = ReactiveCommand.Create<LightWindowBase>(window => window.Close());
            RestoreWindow = ReactiveCommand.Create<LightWindowBase>(window => window.WindowState = WindowState.Normal);
            MaximizeWindow = ReactiveCommand.Create<LightWindowBase>(window => window.WindowState = WindowState.Maximized);
            MinimizeWindow = ReactiveCommand.Create<LightWindowBase>(window => window.WindowState = WindowState.Minimized);
            
        }
    }
}