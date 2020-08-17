using Noone.UI.Core;

namespace Noone.UI
{
    public interface IContainerProvider
    {
        IContainer Container => ContainerLocator.Current;
    }
}
