using System;
using System.Reactive;
using System.Threading.Tasks;
using NooneUI.Framework;
using NooneUI.Models;
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
                var result = await messagebox.ShowAsync("系统消息", "尊敬的天笑哥，\r\n您在本店的娱乐金卡余额已不足，为了避免不必要的麻烦，\r\n以及能够享受到更好的服务，请尽快充值",
                    container.Get<MessageBoxSettings>().Setup(
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
