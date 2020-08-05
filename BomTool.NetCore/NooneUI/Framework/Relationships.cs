using System;
using System.Collections.Generic;
using System.Linq;

namespace NooneUI.Framework
{
    internal class Relationships : IContainerProvider
    {
        public static Relationships Current { get; private set; }

        private readonly Dictionary<Type, Type> map;

        static Relationships()
        {
            Current = new Relationships();
        }

        private Relationships() => map = new Dictionary<Type, Type>();

        public void AddRegistration(IEnumerable<Type> types)
        {
            var viewTypes = types.Where(type => typeof(IView).IsAssignableFrom(type)).ToList();
            var viewModelTypes = types.Where(type => typeof(IViewModel).IsAssignableFrom(type)).ToList();
            viewTypes.ForEach(viewType =>
            {
                var viewModelName = viewType.Name.EndsWith("View") ? viewType.Name + "Model" : viewType.Name + "ViewModel";
                var viewModelType = viewModelTypes.FirstOrDefault(type => type.Name == viewModelName);
                if (viewModelType != null)
                {
                    map.Add(viewType, viewModelType);
                }
            });
        }

        public Type Lookup(Type inputType)
        {
            if (typeof(IView).IsAssignableFrom(inputType))
            {
                return map.FirstOrDefault(e => e.Key == inputType).Value;
            }
            else if (typeof(IViewModel).IsAssignableFrom(inputType))
            {
                return map.FirstOrDefault(e => e.Value == inputType).Key;
            }
            throw new ArgumentException(nameof(inputType));
        }

        public IView GetView(IViewModel viewModel)
        {
            var viewModelType = viewModel.GetType();

            Type viewType = Lookup(viewModelType);

            if (viewType == null)
            {
                return null;
            }

            var view = ((IContainerProvider)this).Container.Get(viewType) as IView;
            if (view != null)
            {
                view.DataContext = viewModel;
            }
            return view;
        }

        public IViewModel GetViewModel(IView view)
        {
            var viewType = view.GetType();

            Type viewModelType = Lookup(viewType);
            if (viewModelType == null)
            {
                return null;
            }
            var viewModel = ((IContainerProvider)this).Container.Get(viewModelType) as IViewModel;
            if (viewModel != null)
            {
                view.DataContext = viewModel;
            }
            return viewModel;
        }
    }
}