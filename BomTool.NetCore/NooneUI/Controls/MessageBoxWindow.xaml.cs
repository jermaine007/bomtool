using Avalonia.Markup.Xaml;
using NooneUI.Framework;

namespace NooneUI.Controls
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
