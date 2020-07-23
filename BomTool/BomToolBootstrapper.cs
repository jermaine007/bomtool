using BomTool.Models;
using NooneUI;
using NooneUI.Logging;
using NooneUI.Pdf;
using System;
using System.Collections.Generic;
using System.Text;

namespace BomTool
{
    /// <summary>
    /// Startup class
    /// </summary>
    class BomToolBootstrapper : Bootstrapper
    {

        /// <summary>
        /// Override to register services
        /// </summary>
        protected override void RegisterServices()
        {
            var logger = ServicesContainer.Get<ILogger>();
            logger.Info("Welcome to use Bom tool");
            logger.Info("Begin to register services");

            this.ServicesContainer.Bind<ExcelReader>(true);
            this.ServicesContainer.Bind<ExcelWriter>(true);
            this.ServicesContainer.Bind<HistoryEntry>(true);
            this.ServicesContainer.Bind<PdfDataGenerator>(true);
            this.ServicesContainer.Bind<PdfPreviewer>(true);
        }
    }
}
