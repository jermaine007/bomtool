namespace NooneUI.Framework
{
    public interface ILoggerProvider
    {
        ILogger Logger => LightContainer.Current.Get<ILogger>();
    }
}