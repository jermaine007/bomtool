using NooneUI.Framework;

namespace NooneUI.Models
{
    /// <summary>
    /// MessageBox settings, would be auto register into ioc container
    /// </summary>
    public class MessageBoxSettings : IAutoRegister
    {
        /// <summary>
        /// Message Box title
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Message box window width
        /// </summary>
        public int Width { get; set; } = 400;

        /// <summary>
        /// Message box height
        /// </summary>
        public int Height { get; set; } = 200;

        /// <summary>
        /// Message box buttons
        /// </summary>
        public MessageBoxButtons Buttons { get; set; } = MessageBoxButtons.OK;

        /// <summary>
        /// Message box icons
        /// </summary>
        public MessageBoxIcons Icons { get; set; } = MessageBoxIcons.None;

        /// <summary>
        /// The message would be displayed in the message box
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
