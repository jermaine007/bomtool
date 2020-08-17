using System;
using System.Reactive;
using System.Threading.Tasks;
using Noone.UI;
using Noone.UI.Core;
using Noone.UI.Models;
using Noone.UI.ViewModels;
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
            OpenFileCommand = ReactiveCommand.Create<MainWindowViewModel>(async (mainViewModel) =>
            {
                var files = await dialog.OpenFileDialogAsync("Select a excel file", "Excel files|xls;xlsx,All files|*");
                if ((files?.Length ?? 0) == 0)
                {
                    return;
                }
                mainViewModel.FileList.AddFile(files[0]);
            });

            ShowAboutCommand = ReactiveCommand.Create<AboutWindowViewModel>(async (window) =>
            {
                await window?.ShowDialog();
            });

            MessageBoxCommand = ReactiveCommand.Create(async () =>
            {
                var result = await messagebox.ShowAsync("系统消息", "尊敬的天笑哥，\r\n您在本店的娱乐金卡余额已不足，为了避免不必要的麻烦，\r\n以及能够享受到更好的服务，请尽快充值",
                    container.Get<MessageBoxSettingsViewModel>().Setup(
                        settings =>
                        {
                            settings.Buttons = MessageBoxButtons.OKCancel;
                            settings.Icons = MessageBoxIcons.Information;
                        }
                ));
            });
        }
    }
}
