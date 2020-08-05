using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace NooneUI.Framework
{
    public class ViewModelBase : ReactiveObject, ILoggerProvider, IContainerProvider, IViewModel
    {
        public IDialogSerivce DialogService { get; }

        public ILogger Logger { get; }
        
        public IContainer Container { get; }

        public IView View => Container.Get<IMvvmRelationships>().GetView(this);

        protected ViewModelBase()
        {
            Container = ((IContainerProvider)this).Container;
            Logger = ((ILoggerProvider)this).Logger.Configure(this);
            DialogService = Container.Get<IDialogSerivce>();
        }
      
    }
}
