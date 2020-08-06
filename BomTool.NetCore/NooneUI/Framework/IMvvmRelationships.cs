using System;

namespace NooneUI.Framework
{
    internal interface IMvvmRelationships
    {
        IView GetView(IViewModel viewModel);
        IViewModel GetViewModel(IView view);
        Type Lookup(Type inputType);
        void Register();
    }
}
