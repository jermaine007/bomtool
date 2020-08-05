using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace NooneUI.Framework
{
    public class ViewModelBase : ReactiveObject, IContainerProvider, ILoggerProvider, IViewModel
    {
        private IView view;
        public IDialogSerivce DialogService => container.Get<IDialogSerivce>();

        protected readonly ILogger logger;
        protected readonly Container container;

        protected ViewModelBase()
        {
            logger = (this as ILoggerProvider).Logger;
            container = (this as IContainerProvider).Container;
        }

        public IView View
        {
            get
            {
                return view ??= Relationships.Current.GetView(this);
            }
            set
            {
                view = value;
            }
        }
    }
}
