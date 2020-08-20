using System;

namespace Noone.UI.Core
{
    /// <summary>
    /// Indicates that a class would be auto registered into IoC container.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AutoRegisterAttribute : Attribute
    {
        /// <summary>
        /// If it is registered as singleton
        /// </summary>
        public bool Singleton { get; set; } = false;

        /// <summary>
        /// The interface type
        /// </summary>
        /// <value></value>
        public Type InterfaceType { get; set; }

    }
}
