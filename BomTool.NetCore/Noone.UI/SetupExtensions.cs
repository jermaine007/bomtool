using System;

namespace Noone.UI
{
    /// <summary>
    /// Setup Extensions, indicate the view or view model could invoke Setup to do initialization
    /// </summary>
    public static class SetupExtensions
    {
        public static T Setup<T>(this T vm, Action<T> setup) where T : ICanSetup
        {
            setup(vm);
            return vm;
        }
    }
}
