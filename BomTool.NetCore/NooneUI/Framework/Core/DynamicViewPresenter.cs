using System;
using System.IO;
using Avalonia.Markup.Xaml;

namespace NooneUI.Framework.Core
{
    [AutoRegister(Singleton = true, InterfaceType = typeof(IDynamicViewPresenter))]
    internal class DynamicViewPresenter : IBaseServiceProvider, IDynamicViewPresenter
    {
        private readonly ITemplateEngine templateEngine;
        private readonly IContainer container;
        private readonly ILogger logger;

        public DynamicViewPresenter()
        {
            container = ((IContainerProvider)this).Container;
            logger = ((ILoggerProvider)this).Logger;
            templateEngine = container.Get<ITemplateEngine>();
        }

        /// <summary>
        /// <inheritdoc cref="IDynamicViewPresenter.GetView(IDynamicViewModel)"/>
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public IView GetView(IDynamicViewModel vm)
        {
            string template = vm.Template;
            logger.Debug($"Try to parse template {template}");

            // Get template and datasource, check if both of them is not null. 
            if (!File.Exists(template) || vm.DataSource == null)
            {
                logger.Error("Template does not exist or data source is null");
                return null;
            }

            // Render xaml content
            string xaml = templateEngine.Render(template, vm.DataSource);

            try
            {
                // dynamically load xaml
                object view = AvaloniaXamlLoader.Parse(xaml);

                if (view is IView control)
                {
                    // binding datacontext
                    control.DataContext = vm;
                    return control;
                }

                logger.Error($"Dynamic view must be a instance of {typeof(DynamicViewBase).FullName}");
            }
            catch (Exception e)
            {
                logger.Error($"Could not parse xaml template dynamically, {e.StackTrace}");
            }
            return null;
        }
    }
}