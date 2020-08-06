using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI.Framework
{
    public interface IMessageBoxProvider
    {
        IMessageBox MessageBox { get; }
    }
}
