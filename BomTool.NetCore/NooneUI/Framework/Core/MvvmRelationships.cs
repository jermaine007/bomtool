using Avalonia.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NooneUI.Framework
{
    internal class MvvmRelationships : ILoggerProvider, IContainerProvider, IMvvmRelationships
    {

        private readonly Dictionary<Type, Type> map;
        public IContainer Container { get; }
        public ILogger Logger { get; }

        public MvvmRelationships()
        {
            map = new Dictionary<Type, Type>();
            Container = ((IContainerProvider)this).Container;
            Logger = ((ILoggerProvider)this).Logger.Configure(this);
        }

        public void Register()
        {
            Logger.Debug($"Registering all view and viewmodel types from {AppDomain.CurrentDomain.BaseDirectory}");
            var types = AppDomain.CurrentDomain.GetAssemblies()
               .Where(assembly => !assembly.IsDynamic && assembly != typeof(LightApplicationBase).Assembly)
               .SelectMany(assembly => assembly.GetExportedTypes())
               .Where(type => typeof(IView).IsAssignableFrom(type) || typeof(IViewModel).IsAssignableFrom(type))
               .ToList();

            types.ToList().ForEach(type =>
            {
                Logger.Debug($"Register type -> {type}");
                Container.Bind(type);
            });
            AddRegistration(types);
        }

        public Type Lookup(Type inputType)
        {
            Logger.Debug($"Lookup mvvm relationship for {inputType}");
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
            Logger.Debug($"Try to get view for {viewModelType}");
            Type viewType = Lookup(viewModelType);

            if (viewType == null)
            {
                return null;
            }

            var view = ((IContainerProvider)this).Container.Get(viewType) as IView;
            if (view != null)
            {
                Logger.Debug($"Found view: {view}");
                view.DataContext = viewModel;
            }
            return view;
        }

        public IViewModel GetViewModel(IView view)
        {
            var viewType = view.GetType();
            Logger.Debug($"Try to get view for {viewType}");
            Type viewModelType = Lookup(viewType);
            if (viewModelType == null)
            {
                return null;
            }
            var viewModel = ((IContainerProvider)this).Container.Get(viewModelType) as IViewModel;
            if (viewModel != null)
            {
                Logger.Debug($"Found viewmodel: {view}");
                view.DataContext = viewModel;
            }
            return viewModel;
        }

        private void AddRegistration(IEnumerable<Type> types)
        {
            var viewTypes = types.Where(type => typeof(IView).IsAssignableFrom(type)).ToList();
            var viewModelTypes = types.Where(type => typeof(IViewModel).IsAssignableFrom(type)).ToList();
            viewTypes.ForEach(viewType =>
            {
                var viewModelName = viewType.Name.EndsWith("View") ? viewType.Name + "Model" : viewType.Name + "ViewModel";
                var viewModelType = viewModelTypes.FirstOrDefault(type => type.Name == viewModelName);
                if (viewModelType != null)
                {
                    Logger.Debug($"Add mvvm relationship: [{viewType} , {viewModelType}]");
                    map.Add(viewType, viewModelType);
                }
                else
                {
                    Logger.Debug($"Could not found viewmodel type for {viewType}");
                }
            });

        }
    }
}