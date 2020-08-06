using Avalonia;

namespace NooneUI.Framework
{
    /// <summary>
    /// View interface
    /// </summary>
    public interface IView : IDataContextProvider, ICanRegister
    {
        string Id => this.GetType().FullName;
    }
}
