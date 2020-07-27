using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BomTool.NetCore.Views
{
    public class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}