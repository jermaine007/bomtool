using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI.Framework
{
    public interface IDialogProvider
    {
        IDialog Dialog => ContainerLocator.Current.Get<IDialog>();
    }
}
