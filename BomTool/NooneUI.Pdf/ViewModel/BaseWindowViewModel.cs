using GalaSoft.MvvmLight;
using NooneUI.Pdf.View;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;

namespace NooneUI.Pdf.ViewModel
{
    abstract public class BaseWindowViewModel : ViewModelBase
    {
        public object Host { get; private set; }

        protected BaseWindowViewModel()
        {
            var attribute = this.GetType().GetCustomAttribute<ViewLocatorAttribute>();

            if (attribute == null)
            {
                throw new NotImplementedException();
            }

            this.Host = Activator.CreateInstance(attribute.Type);
            if (this.Host is FrameworkElement frameworkElement)
            {
                frameworkElement.DataContext = this;
            }
        }


        public virtual void Show()
        {
            if (this.Host is Window win)
            {
                win.ShowDialog();
            }
        }


        public virtual void Close()
        {
            if (this.Host is Window win)
            {
                win.Close();
            }
        }

        public virtual void BringToFront()
        {

            if (this.Host is Window win)
            {
                //win.Topmost = true;
                win.Activate();
            }
        }
    }
}
