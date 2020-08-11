using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace NooneUI.Framework
{
    public abstract class ViewModelBase : ReactiveObject, IServiceProvider, IViewModel
    {
        protected readonly IDialog dialog;
        protected readonly IMessageBox messagebox;
        protected readonly ILogger logger;
        protected readonly IContainer container;

        protected ViewModelBase()
        {
            container = ((IServiceProvider)this).Container;
            logger = ((IServiceProvider)this).Logger;
            dialog = ((IServiceProvider)this).Dialog;
            messagebox = ((IServiceProvider)this).MessageBox;
            logger.Debug($"ViewModel:{((IHaveId)this).Id} has been created");
        }

    }
}
