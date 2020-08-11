using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRegisterAttribute : Attribute
    {
        public bool Singleton { get; set; } = false;

        public Type InterfaceType { get; set; }

    }
}
