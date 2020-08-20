using Noone.UI.Core;

namespace Noone.UI
{
    public interface IMessageBoxProvider
    {
        IMessageBox MessageBox => ContainerLocator.Current.Get<IMessageBox>();
    }
}
