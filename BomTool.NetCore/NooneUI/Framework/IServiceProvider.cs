using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI.Framework
{
    public interface IServiceProvider : IBaseServiceProvider, IDialogProvider, IMessageBoxProvider
    {
        IDialog IDialogProvider.Dialog => Container.Get<IDialog>();
        IMessageBox IMessageBoxProvider.MessageBox => Container.Get<IMessageBox>();
    }
}
