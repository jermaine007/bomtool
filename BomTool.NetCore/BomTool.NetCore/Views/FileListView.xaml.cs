using Avalonia.Markup.Xaml;
using NooneUI.Framework;

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
