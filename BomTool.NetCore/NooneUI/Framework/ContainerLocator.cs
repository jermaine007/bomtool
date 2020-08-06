using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI.Framework
{
    public static class ContainerLocator
    {
        static ContainerLocator() => Configure(() => LightContainer.Instance);

        internal static IContainer Current { get; private set; }

        public static void Configure(Func<IContainer> factory) => Current = factory();
    }

}
