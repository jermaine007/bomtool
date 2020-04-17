using Ninject.Modules;
using NooneUI.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI.Services
{
    internal class NooneUINInjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().To<NooneUILogger>().InSingletonScope();
        }
    }
}
