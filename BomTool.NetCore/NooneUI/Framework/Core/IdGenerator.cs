using System;
using System.Collections.Generic;

namespace NooneUI.Framework
{
    [AutoRegister(Singleton = true)]
    internal class IdGenerator : IAutoRegister
    {
        private readonly Dictionary<string, long> map;

        public IdGenerator() => map = new Dictionary<string, long>();

        public string Next(Type type)
        {
            var name = type.FullName;
            if (!map.TryGetValue(name, out var id))
            {
                id = 1;
                map.Add(name, id);
            }
            else
            {
                id = id + 1;
                map[name] = id;
            }
            return $"{name}{id}";
        }
    }
}
