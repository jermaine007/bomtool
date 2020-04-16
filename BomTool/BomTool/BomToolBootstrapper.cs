using BomTool.Models;
using NooneUI;
using NooneUI.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BomTool
{
    class BomToolBootstrapper : Bootstrapper
    {

        protected override void RegisterServices()
        {
            var logger = ServicesContainer.Get<ILogger>();
            logger.Info("Welcome to use Bom tool");
            logger.Info("Begin to register services");
            this.ServicesContainer.Bind<ExcelData>();
            this.ServicesContainer.Bind<ExcelGrouppedData>();
            this.ServicesContainer.Bind<ExcelReader>(true);
            this.ServicesContainer.Bind<ExcelWriter>(true);
            this.ServicesContainer.Bind<HistoryEntry>(true);
        }
    }
}
