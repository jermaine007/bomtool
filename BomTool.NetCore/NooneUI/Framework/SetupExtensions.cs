using System;

namespace NooneUI.Framework
{
    public static class SetupExtensions
    {
        public static T Setup<T>(this T vm, Action<T> setup) where T : ICanSetup
        {
            setup(vm);
            return vm;
        }
    }
}
