namespace NooneUI.Framework
{
    /// <summary>
    /// View model interface
    /// </summary>
    public interface IViewModel : IAutoRegister
    {
        /// <summary>
        /// Relate view
        /// </summary>
        /// <value></value>
        IView View { get; }
    }
}
