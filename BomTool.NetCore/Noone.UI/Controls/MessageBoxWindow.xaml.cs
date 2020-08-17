using Avalonia.Markup.Xaml;

namespace Noone.UI.Controls
{
    public class MessageBoxWindow : LightWindowBase
    {
        public MessageBoxWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
