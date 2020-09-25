using Avalonia.Markup.Xaml;
using Noone.UI.Controls;
using System;
using System.IO;

namespace Noone.UI.Core
{
    /// <summary>
    /// Dynamic view presenter, generate the view by specified template and data source.
    /// </summary>
    [AutoRegister(Singleton = true, InterfaceType = typeof(IDynamicViewPresenter))]
    internal class DynamicViewPresenter : IContainerProvider,
        ILoggerProvider,
        ITemplateEngineProvider,
        IDynamicViewPresenter
    {
        private readonly ITemplateEngine templateEngine;
        private readonly ILogger logger;

        public DynamicViewPresenter()
        {
            logger = ((ILoggerProvider)this).Logger;
            templateEngine = ((ITemplateEngineProvider)this).TemplateEngine;
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

            // Get template, check if template is not null. 
            if (!File.Exists(template))
            {
                logger.Error("Template does not exist is null");
                return null;
            }

            // Render xaml content
            string xaml = templateEngine.Render(template, vm.DataSource);

            try
            {
                // https://github.com/verybadcat/CSharpMath/pull/149
                // moved into the separate package: Avalonia.Markup.Xaml.Loader
                // dynamically load xaml
                object view = AvaloniaRuntimeXamlLoader.Parse(xaml);

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
