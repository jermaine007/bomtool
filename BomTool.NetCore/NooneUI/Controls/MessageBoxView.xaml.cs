using Avalonia.Markup.Xaml;
using NooneUI.Framework;

namespace NooneUI.Controls
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
