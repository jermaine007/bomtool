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
            Bind<NooneUILogger, ILogger>().To<NooneUILogger>().InSingletonScope();
        }
    }
}
