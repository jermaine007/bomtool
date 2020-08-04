using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace NooneUI.Framework
{
    public class ViewModelBase : ReactiveObject, IContainerProvider, ILoggerProvider, IViewModel
    {

        public IDialogSerivce DialogService => container.Get<IDialogSerivce>();

        protected readonly ILogger logger;
        protected readonly Container container;

        protected ViewModelBase()
        {
            logger = (this as ILoggerProvider).Logger;
            container = (this as IContainerProvider).Container;
        }

        public IView View { get; set; }
    }
}
