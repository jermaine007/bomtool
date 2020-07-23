using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BomTool.Models
{
    /// <summary>
    /// Class provides mvvm style base feature
    /// </summary>
    abstract public class BindableBase : Dispatchable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool SetProperty<T>(ref T storage, T newValue, [CallerMemberName]string propertyName = "", Action<string, T, T> callback = null)
        {
            if (Equals(storage, newValue))
            {
                return false;
            }
            var oldValue = storage;
            storage = newValue;
            Action execution = () =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                callback?.Invoke(propertyName, newValue, oldValue);
            };
            if (IsMainThread)
            {
                execution();
            }
            else
            {
                Dispatch(execution);
            }
            return true;
        }
    }
}
