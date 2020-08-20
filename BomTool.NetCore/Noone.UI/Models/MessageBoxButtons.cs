using System;

namespace Noone.UI.Models
{

    /// <summary>
    /// Message box buttons
    /// </summary>
    [Flags]
    public enum MessageBoxButtons : ulong
    {
        None = MessageBoxButtonsBase.None,
        /// <summary>
        ///  OK button
        /// </summary>
        OK = MessageBoxButtonsBase.OK,

        /// <summary>
        ///  Cancel button
        /// </summary>
        Cancel = MessageBoxButtonsBase.Cancel,
        /// <summary>
        /// OK and Cancel button
        /// </summary>
        OKCancel = MessageBoxButtonsBase.OK | MessageBoxButtonsBase.Cancel,
        /// <summary>
        /// Yes and No button
        /// </summary>
        YesNo = MessageBoxButtonsBase.Yes | MessageBoxButtonsBase.No,
        /// <summary>
        /// Yes, No and Cancel button
        /// </summary>
        YesNoCancel = MessageBoxButtonsBase.Yes | MessageBoxButtonsBase.No | MessageBoxButtonsBase.Cancel
    }
}
