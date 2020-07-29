using System;

namespace NooneUI.Framework
{
    [Flags]
    public enum SystemButtons : ulong
    {
        None = 1 << 0,
        Close = 1 << 1,
        Minimize = 1 << 2,
        Maximize = 1 << 3,
        MinimizeClose = Minimize | Close,
        MaximizeClose = Maximize | Close,
        All = Close | Minimize | Maximize
    }
}