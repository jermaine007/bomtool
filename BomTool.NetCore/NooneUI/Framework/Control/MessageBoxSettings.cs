namespace NooneUI.Framework
{
    public class MessageBoxSettings
    {
        public int Width { get; set; } = 400;
        public int Height { get; set; } = 200;

        public MessageBoxButtons Buttons { get; set; } = MessageBoxButtons.OK;
        public MessageBoxIcons Icons { get; set; } = MessageBoxIcons.None;

    }
}
