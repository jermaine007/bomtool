using Noone.UI.Core;

namespace Noone.UI
{
    /// <summary>
    /// Indicate that the class which implement this interface has Id
    /// </summary>
    public interface ICanSetup
    {
        string Id => ContainerLocator.Current.Get<IdGenerator>().Next(this.GetType().FullName,"{0}#{1}");
    }
}
