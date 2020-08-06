using System;
using System.Reactive;
using System.Threading.Tasks;
using NooneUI.Framework;
using ReactiveUI;

namespace BomTool.NetCore.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public ReactiveCommand<MainWindowViewModel, Unit> OpenFileCommand { get; }
        public ReactiveCommand<AboutWindowViewModel, Unit> ShowAboutCommand { get; }
        public ReactiveCommand<Unit, Task> MessageBoxCommand { get; }

        public MenuViewModel()
        {
            OpenFileCommand = ReactiveCommand.Create<MainWindowViewModel>(async (window) =>
            {
                var files = await dialog.OpenFileDialogAsync();
            });

            ShowAboutCommand = ReactiveCommand.Create<AboutWindowViewModel>(async (window) =>
            {
                await window?.ShowDialog();
            });

            MessageBoxCommand = ReactiveCommand.Create(async () =>
            {
                var result = await messagebox.ShowAsync("test");
                return;
            });
        }
    }
}
