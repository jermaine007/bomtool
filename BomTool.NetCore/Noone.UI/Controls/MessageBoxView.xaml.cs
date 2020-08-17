using Avalonia.Markup.Xaml;
using Noone.UI.Core;

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
