using System;
using System.Collections.Generic;
using System.Text;

namespace Noone.UI.Core
{
    /// <summary>
    /// IoC Container locator, just like ServiceLocator, but not provide to users, only use internal.
    /// Could change IoC Container instance by <see cref="BootstrapperExtensions.Use(Bootstrapper, Func{IContainer})"/>
    /// </summary>
    internal static class ContainerLocator
    {
        /// <summary>
        /// Current <see cref="IContainer"/> instance.
        /// </summary>
        private static volatile IContainer current;

        /// <summary>
        /// Current <see cref="IContainer"/> instance, if <see cref="current"/> is not set or is null,
        /// would use <see cref="LightContainer.Instance"/> as default.
        /// </summary>
        internal static IContainer Current => current ??= LightContainer.Instance;

        /// <summary>
        /// Method use to for create an <see cref="IContainer"/> instance.
        /// </summary>
        /// <param name="factory">Factory to create a <see cref="IContainer"/> instance</param>
        internal static void Configure(Func<IContainer> factory) => current = factory?.Invoke();
    }

}
