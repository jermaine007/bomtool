using Noone.UI.Core;

namespace Noone.UI
{
    public interface IDialogProvider
    {
        IDialog Dialog => ContainerLocator.Current.Get<IDialog>();
    }
}
