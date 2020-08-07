using Avalonia;

namespace NooneUI.Framework
{
    /// <summary>
    /// View interface
    /// </summary>
    public interface IView : IDataContextProvider, IAutoRegister
    {
        string Id => this.GetType().FullName;
    }
}
