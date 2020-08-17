using Avalonia.Markup.Xaml;
using Noone.UI.Controls;
using Noone.UI.Core;

namespace BomTool.NetCore.Views
{
    public class FileContentView : LightUserControlBase
    {
        public FileContentView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
