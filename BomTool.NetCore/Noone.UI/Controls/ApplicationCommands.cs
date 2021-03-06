using Avalonia.Controls;
using Noone.UI.Models;
using ReactiveUI;
using System.Reactive;

namespace Noone.UI.Controls
{

    /// <summary>
    /// Class provides system menu functions like close, maximize, minimize etc.
    /// Auto binding in <see cref="LightWindowBase"/>
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

        /// <summary>
        /// Toggle windows
        /// </summary>
        public static ReactiveCommand<LightWindowBase, Unit> ToggleWindow { get; private set; }

        static ApplicationCommands()
        {
            CloseWindow = ReactiveCommand.Create<LightWindowBase>(window =>
            {
                // if a window view model with dialog result, close with result, otherwise close directly
                if (window.DataContext is IWindowViewModel vm)
                {
                    // always use vm to close window
                    vm.Close();
                }
            });
            RestoreWindow = ReactiveCommand.Create<LightWindowBase>(window => window.WindowState = WindowState.Normal);
            MaximizeWindow = ReactiveCommand.Create<LightWindowBase>(window => window.WindowState = WindowState.Maximized);
            MinimizeWindow = ReactiveCommand.Create<LightWindowBase>(window => window.WindowState = WindowState.Minimized);
            ToggleWindow = ReactiveCommand.Create<LightWindowBase>(window =>
            {
                if (window.SystemButtons.HasFlag(SystemButtons.Maximize))
                {
                    window.WindowState ^= WindowState.Maximized;
                }
            });
        }
    }
}
