using Avalonia.Controls;
using ReactiveUI;
using System.Reactive;

namespace NooneUI.Framework
{

    /// <summary>
    /// Class provides system menu functions like close, maximize, minimize etc.
    /// </summary>
    public class ApplicationCommands
    {
        /// <summary>
        /// Close window command
        /// </summary>
        public static ReactiveCommand<LightWindowBase, Unit> CloseWindow { get; private set; }

        /// <summary>
        /// Restore window command
        /// </summary>
        public static ReactiveCommand<LightWindowBase, Unit> RestoreWindow { get; private set; }

        /// <summary>
        /// Maximize window command
        /// </summary>
        public static ReactiveCommand<LightWindowBase, Unit> MaximizeWindow { get; private set; }

        /// <summary>
        /// Minimize window command
        /// </summary>
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