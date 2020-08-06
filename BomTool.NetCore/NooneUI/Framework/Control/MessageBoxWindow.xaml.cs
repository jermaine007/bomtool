using Avalonia.Markup.Xaml;

namespace NooneUI.Framework
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
