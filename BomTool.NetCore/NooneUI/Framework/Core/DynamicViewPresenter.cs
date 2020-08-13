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

        public IView GetView(IDynamicViewModel vm)
        {
            string template = vm.Template;
            logger.Debug($"Try to parse template {template}");
            if (!File.Exists(template) || vm.DataSource == null)
            {
                logger.Error("Template does not exist or data source is null");
                return null;
            }

            string xaml = templateEngine.Render(template, vm.DataSource);
            try
            {
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
