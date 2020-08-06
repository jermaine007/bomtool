using Avalonia.Markup.Xaml;
using Ninject.Modules;

namespace NooneUI.Framework
{
    internal class LightMoudle : NinjectModule
    {
        public override void Load()
        {
            Bind<LightLogger, ILogger>().To<LightLogger>();
            Bind<LightDialog, IDialog>().To<LightDialog>().InSingletonScope();
            Bind<ViewPresenter, IViewPresenter>().To<ViewPresenter>().InSingletonScope();
            Bind<MvvmRelationships, IMvvmRelationships>().To<MvvmRelationships>().InSingletonScope();

            Bind<ViewLocator>().ToSelf().InSingletonScope();
        }
    }
}
