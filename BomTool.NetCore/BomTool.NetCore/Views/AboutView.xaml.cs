using Avalonia.Markup.Xaml;
using Noone.UI.Controls;

namespace BomTool.NetCore.Views
{
    public class AboutView : LightUserControlBase
    {
        public AboutView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
