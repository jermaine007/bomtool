namespace NooneUI.Framework
{
    public interface ILoggerProvider
    {
        ILogger Logger => ContainerLocator.Current.Get<ILogger>().Configure(this);
    }
}
