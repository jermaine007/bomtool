using System;

namespace NooneUI.Framework
{
    public static class MvvmExtensions
    {
        public static T With<T>(this T vm, Action<T> setup) where T : IAutoRegister
        {
            setup(vm);
            return vm;
        }
    }
}
