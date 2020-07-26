using Avalonia.Markup.Xaml;
using Ninject.Modules;

namespace BomTool.NetCore.Framework
{
    public class Moudle : NinjectModule
    {
        public override void Load() =>
            Bind<LightLogger, ILogger>().To<LightLogger>().InSingletonScope();
    }
}