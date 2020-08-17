using System;
using System.Collections.Generic;

namespace Noone.UI
{
    internal interface IMvvmRelationships
    {
        IView GetView(IViewModel viewModel);
        IViewModel GetViewModel(IView view);
        Type Lookup(Type inputType);
        void AddRegistration(IEnumerable<Type> types);
    }
}
