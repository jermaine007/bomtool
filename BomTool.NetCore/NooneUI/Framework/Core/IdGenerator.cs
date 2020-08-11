using System;
using System.Collections.Generic;

namespace NooneUI.Framework
{
    /// <summary>
    /// A class use to generate id by specified prefix.
    /// </summary>
    [AutoRegister(Singleton = true)]
    internal class IdGenerator : ICanAutoRegister
    {
        /// <summary>
        /// Map to store ids according to prefix
        /// </summary>
        private readonly Dictionary<string, long> map;

        public IdGenerator() => map = new Dictionary<string, long>();

        /// <summary>
        /// Get the next id for specified prefix
        /// </summary>
        /// <param name="prefix">specified prefix</param>
        /// <param name="format">string format which meets <see cref="string.Format(string, object?[])"/></param>
        /// <returns></returns>
        public string Next(string prefix, string format = "{0}{1}")
        {
            if (!map.TryGetValue(prefix, out var id))
            {
                id = 0;
            }
            map[prefix] = ++id;
            return string.Format(format, prefix, id);
        }
    }
}
