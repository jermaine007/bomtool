using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace NooneUI.Framework
{
    [AutoRegister]
    public abstract class ViewModelBase : ReactiveObject,
        ILoggerProvider,
        IContainerProvider,
        IDialogProvider,
        IMessageBoxProvider,
        IViewModel
    {
        protected readonly IDialog dialog;
        protected readonly IMessageBox messagebox;
        protected readonly ILogger logger;
        protected readonly IContainer container;

        protected ViewModelBase()
        {
            container = ((IContainerProvider)this).Container;
            logger = ((ILoggerProvider)this).Logger;
            dialog = ((IDialogProvider)this).Dialog;
            messagebox = ((IMessageBoxProvider)this).MessageBox;
            logger.Debug($"ViewModel:{((ICanSetup)this).Id} has been created");
        }

    }
}
