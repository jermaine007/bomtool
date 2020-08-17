using BomTool.NetCore.Models;
using Noone.UI.Core;
using Noone.UI.ViewModels;

namespace BomTool.NetCore.ViewModels
{
    public class AboutWindowViewModel : WindowViewModelBase
    {

        private AboutInfo aboutInfo;
        public AboutInfo Info  => aboutInfo??= AboutInfo.Load();
    }
}
