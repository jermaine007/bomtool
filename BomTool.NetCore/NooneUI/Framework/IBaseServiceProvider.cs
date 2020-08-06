using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI.Framework
{
    public interface IBaseServiceProvider : ILoggerProvider, IContainerProvider
    {
        IContainer IContainerProvider.Container => ContainerLocator.Current;

        ILogger ILoggerProvider.Logger => Container.Get<ILogger>().Configure(this);
    }
}
