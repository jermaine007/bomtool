namespace BomTool.NetCore.Framework
{
    public interface ILoggerProvider
    {
        ILogger Logger => Container.Current.Get<ILogger>();
    }
}