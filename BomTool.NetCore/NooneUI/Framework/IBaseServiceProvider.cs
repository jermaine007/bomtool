using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI.Framework
{
    /// <summary>
    /// A base service interface to provide logger and ioc container services,
    /// Use interface default implementation, more information about default more implementation
    /// Please check https://devblogs.microsoft.com/dotnet/default-implementations-in-interfaces/
    /// </summary>
    public interface IBaseServiceProvider : ILoggerProvider, IContainerProvider
    {
        /// <summary>
        /// IContainerProvider default implementation
        /// </summary>
        IContainer IContainerProvider.Container => ContainerLocator.Current;

        /// <summary>
        /// ILoggerProvider default implementation
        /// </summary>
        ILogger ILoggerProvider.Logger => Container.Get<ILogger>().Configure(this);
    }
}
