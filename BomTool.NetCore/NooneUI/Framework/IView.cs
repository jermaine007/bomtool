using Avalonia;

namespace NooneUI.Framework
{
    /// <summary>
    /// View interface
    /// </summary>
    public interface IView : IDataContextProvider
    {
        string Id => this.GetType().FullName;
    }
}
