using System;

namespace NooneUI.Framework
{
    public static class AutoRegisterExtensions
    {
        public static T Setup<T>(this T vm, Action<T> setup) where T : ICanAutoRegister
        {
            setup(vm);
            return vm;
        }
    }
}
