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
        protected readonly LightContainer container;

        protected ViewModelBase()
        {
            logger = ((ILoggerProvider)this).Logger.Configure(this);
            container = ((IContainerProvider)this).Container;
        }

        public IView View => container.Get<MvvmRelationships>().GetView(this);
    }
}
