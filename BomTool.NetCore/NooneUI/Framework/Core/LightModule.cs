using Avalonia.Markup.Xaml;
using Ninject.Modules;

namespace NooneUI.Framework
{
    internal class LightMoudle : NinjectModule
    {
        public override void Load()
        {
            Bind<LightLogger, ILogger>().To<LightLogger>();
            Bind<LightDialogService, IDialogSerivce>().To<LightDialogService>().InSingletonScope();
            Bind<ViewLocator>().ToSelf().InSingletonScope();
            Bind<ViewPresenter>().ToSelf().InSingletonScope();
            Bind<MvvmRelationships>().ToSelf().InSingletonScope();
        }
    }
}