using Avalonia.Markup.Xaml;
using NooneUI.Framework;

namespace BomTool.NetCore.Views
{
    public class StatusBarView : LightUserControlBase
    {
        public StatusBarView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
