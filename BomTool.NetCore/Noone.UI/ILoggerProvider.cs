using Noone.UI.Core;

namespace Noone.UI
{
    public interface ILoggerProvider
    {
        ILogger Logger => ContainerLocator.Current.Get<ILogger>().Configure(this);
    }
}
