using Avalonia.Markup.Xaml;
using Noone.UI.Controls;

namespace BomTool.NetCore.Views
{
    public class FileListView : LightUserControlBase
    {
        public FileListView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
