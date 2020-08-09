namespace NooneUI.Framework
{
    /// <summary>
    /// View model interface
    /// </summary>
    public interface IViewModel : IAutoRegister
    {
        string Id => this.GetType().FullName;
    }
}
