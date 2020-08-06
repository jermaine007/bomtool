namespace NooneUI.Framework
{
    /// <summary>
    /// View model interface
    /// </summary>
    public interface IViewModel : ICanRegister
    {
        /// <summary>
        /// Relate view
        /// </summary>
        /// <value></value>
        IView View { get; }
    }
}
