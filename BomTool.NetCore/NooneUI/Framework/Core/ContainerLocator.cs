using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI.Framework
{
    internal static class ContainerLocator
    {
        private static volatile IContainer current;

        internal static IContainer Current => current ??= LightContainer.Instance;

        public static void Configure(Func<IContainer> factory) => current = factory?.Invoke();
    }

}
