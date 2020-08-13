using Avalonia.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NooneUI.Framework
{
    [AutoRegister(Singleton = true, InterfaceType = typeof(IMvvmRelationships))]
    internal class MvvmRelationships : IContainerProvider,
        ILoggerProvider,
        IMvvmRelationships
    {
        private readonly Dictionary<Type, Type> map;

        protected readonly IContainer container;
        protected readonly ILogger logger;

        public MvvmRelationships()
        {
            map = new Dictionary<Type, Type>();
            container = ((IContainerProvider)this).Container;
            logger = ((ILoggerProvider)this).Logger;
        }

        public Type Lookup(Type inputType)
        {
            logger.Debug($"Lookup mvvm relationship for {inputType}");
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
            logger.Debug($"Try to get view for {viewModelType}");
            Type viewType = Lookup(viewModelType);

            if (viewType == null)
            {
                return null;
            }

            var view = container.Get(viewType) as IView;
            if (view != null)
            {
                logger.Debug($"Found view: {view}");
                view.DataContext = viewModel;
            }
            return view;
        }

        public IViewModel GetViewModel(IView view)
        {
            var viewType = view.GetType();
            logger.Debug($"Try to get view for {viewType}");
            Type viewModelType = Lookup(viewType);
            if (viewModelType == null)
            {
                return null;
            }
            var viewModel = container.Get(viewModelType) as IViewModel;
            if (viewModel != null)
            {
                logger.Debug($"Found viewmodel: {view}");
                view.DataContext = viewModel;
            }
            return viewModel;
        }

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
                    logger.Debug($"Add mvvm relationship: [{viewType} , {viewModelType}]");
                    map.Add(viewType, viewModelType);
                }
                else
                {
                    logger.Debug($"Could not found viewmodel type for {viewType}");
                }
            });

        }
    }
}
