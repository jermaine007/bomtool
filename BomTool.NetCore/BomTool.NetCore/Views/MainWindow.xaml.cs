using Avalonia.Markup.Xaml;
using Noone.UI.Controls;

namespace BomTool.NetCore.Views
{
    public class MainWindow : LightWindowBase
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
