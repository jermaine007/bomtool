using System;

namespace Noone.UI.Models
{
    [Flags]
    internal enum MessageBoxButtonsBase : ulong
    {
        OK = 1 << 0,
        Cancel = 1 << 1,
        Yes = 1 << 2,
        No = 1 << 3
    }
}
