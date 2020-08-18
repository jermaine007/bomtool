using BomTool.NetCore.Models;
using ReactiveUI;
using Noone.UI.Core;
using Noone.UI.ViewModels;
using System.Reactive;
using System.Diagnostics;

namespace BomTool.NetCore.ViewModels
{
    public class AboutViewModel : MessageBoxViewModel
    {

        private AboutInfo aboutInfo;
        public AboutInfo Info => aboutInfo ??= AboutInfo.Load();
        public ReactiveCommand<string, Unit> OpenUrlCommand { get; }
        public AboutViewModel()
        {
            OpenUrlCommand = ReactiveCommand.Create<string>((url) =>
            {
                Process.Start(new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = url
                });
            });
        }
    }
}
