using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace NooneUI.Framework
{
    public class ViewModelBase : ReactiveObject, IServiceProvider, IViewModel
    {
        protected readonly IDialog dialog;
        protected readonly IMessageBox messagebox;
        protected readonly ILogger logger;
        protected readonly IContainer container;
       
        public IView View => container.Get<IMvvmRelationships>().GetView(this);

        protected ViewModelBase()
        {
            container = ((IServiceProvider)this).Container;
            logger = ((IServiceProvider)this).Logger;
            dialog = ((IServiceProvider)this).Dialog;
            messagebox = ((IServiceProvider)this).MessageBox;
        }
      
    }
}
