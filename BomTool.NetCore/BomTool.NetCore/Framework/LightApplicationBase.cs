using Avalonia;
using Avalonia.Markup.Xaml;

namespace BomTool.NetCore.Framework
{
    public class LightApplicationBase : Application, IContainerProvider, ILoggerProvider
    {
        protected readonly ILogger logger;

        protected LightApplicationBase()  => logger = (this as ILoggerProvider).Logger;
        
        public override void Initialize() => RegisterServices((this as IContainerProvider).Container);
        
        protected virtual void RegisterServices(Container container) { }
    }
}