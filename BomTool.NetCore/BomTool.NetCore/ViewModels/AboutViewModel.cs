using BomTool.NetCore.Models;
using Noone.UI.Core;
using Noone.UI.ViewModels;

namespace BomTool.NetCore.ViewModels
{
    public class AboutViewModel : MessageBoxViewModel
    {

        private AboutInfo aboutInfo;
        public AboutInfo Info  => aboutInfo??= AboutInfo.Load();
    }
}
