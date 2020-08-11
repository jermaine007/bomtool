using System;
using System.Collections.Generic;

namespace NooneUI.Framework
{
    internal interface IMvvmRelationships
    {
        IView GetView(IViewModel viewModel);
        IViewModel GetViewModel(IView view);
        Type Lookup(Type inputType);
        void AddRegistration(IEnumerable<Type> types);
    }
}
