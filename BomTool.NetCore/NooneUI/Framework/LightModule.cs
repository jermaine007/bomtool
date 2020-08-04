using Avalonia.Markup.Xaml;
using Ninject.Modules;

namespace NooneUI.Framework
{
    internal class LightMoudle : NinjectModule
    {
        public override void Load()
        {
            Bind<LightLogger, ILogger>().To<LightLogger>().InSingletonScope();
            Bind<LightDialogService, IDialogSerivce>().To<LightDialogService>().InSingletonScope();
        }
    }
}