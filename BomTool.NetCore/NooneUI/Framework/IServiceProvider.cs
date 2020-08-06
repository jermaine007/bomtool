using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI.Framework
{
    public interface IServiceProvider : IBaseServiceProvider, IDialogProvider
    {
        IDialog IDialogProvider.Dialog => Container.Get<IDialog>();
    }
}
