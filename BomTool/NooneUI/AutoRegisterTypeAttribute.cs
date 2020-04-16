using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class AutoRegisterTypeAttribute : Attribute
    {
        public string Uri { get; set; } = "NooneUI";

        public int MajorVersion { get; set; } = 1;

        public int MinorVersion { get; set; } = 0;

        public bool IsSingleton { get; set; } = false;

        public AutoRegisterTypeAttribute()
        {
        }

        public AutoRegisterTypeAttribute(string uri) : this(uri, false)
        {
        }

        public AutoRegisterTypeAttribute(bool isSingleton) : this("NooneUI", isSingleton)
        {
        }

        public AutoRegisterTypeAttribute(string uri, bool isSingleton)
        {
            this.Uri = uri;
            this.IsSingleton = isSingleton;
        }

      
    }
}
