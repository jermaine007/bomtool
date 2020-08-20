using Avalonia.Markup.Xaml;
using Noone.UI.Controls;

namespace BomTool.NetCore.Views
{
    public class MenuView : LightUserControlBase
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
