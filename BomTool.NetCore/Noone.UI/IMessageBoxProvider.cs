using System;
using System.Collections.Generic;
using System.Text;
using Noone.UI.Core;

namespace Noone.UI
{
    public interface IMessageBoxProvider
    {
        IMessageBox MessageBox => ContainerLocator.Current.Get<IMessageBox>();
    }
}
