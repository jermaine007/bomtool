using Avalonia;

namespace NooneUI.Framework
{
    public interface IView : IDataContextProvider
    {
        string Id => this.GetType().FullName;
    }
}