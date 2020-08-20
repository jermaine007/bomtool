using Avalonia.Markup.Xaml;

namespace Noone.UI.Controls
{
    public class MessageBoxView : LightUserControlBase
    {
        public MessageBoxView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
