using Avalonia.Markup.Xaml;
using NooneUI.Framework;

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
