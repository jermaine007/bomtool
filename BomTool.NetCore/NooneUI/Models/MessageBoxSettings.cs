using NooneUI.Framework;

namespace NooneUI.Models
{
    public class MessageBoxSettings : IAutoRegister
    {
        public string Title { get; set; } = string.Empty;
        public int Width { get; set; } = 400;
        public int Height { get; set; } = 200;
        public MessageBoxButtons Buttons { get; set; } = MessageBoxButtons.OK;
        public MessageBoxIcons Icons { get; set; } = MessageBoxIcons.None;
        public string Message { get; set; } = string.Empty;
    }
}
