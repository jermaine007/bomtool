using System;
using System.Collections.Generic;
using System.Linq;

namespace NooneUI.Framework
{
    internal class Relationship
    {
        public static Relationship Current { get; set; }
        private readonly Dictionary<Type, Type> map;

        static Relationship()
        {
            Current = new Relationship();
        }

        private Relationship() => map = new Dictionary<Type, Type>();

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
                return map.ContainsKey(inputType) ? map[inputType] : null;
            }
            else if (typeof(IViewModel).IsAssignableFrom(inputType))
            {
                return map.FirstOrDefault(e => e.Value == inputType).Key;
            }
            throw new ArgumentException(nameof(inputType));
        }
    }
}