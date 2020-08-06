using Avalonia.Markup.Xaml;

namespace NooneUI.Framework
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
