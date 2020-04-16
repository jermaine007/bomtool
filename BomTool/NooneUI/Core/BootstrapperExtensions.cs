using Qml.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI.Core
{
    public static class BootstrapperExtensions
    {
        public static IBootstrapper DetectQtRuntime(this IBootstrapper bootstrapper)
        {
            var loader = new QtRuntimeLoader();
            if (loader.HasBuiltInRuntime)
            {
                (bootstrapper as Bootstrapper).ResolveQtRuntime = loader.Load;
            }
            return bootstrapper;
        }

        public static IBootstrapper RegisterTypes(this IBootstrapper bootstrapper, Action doRegisterTypes)
        {
            (bootstrapper as Bootstrapper).DoRegisterTypes = doRegisterTypes;
            return bootstrapper;
        }

        public static IBootstrapper SetStyle(this IBootstrapper bootstrapper, string style = "Materail")
        {
            (bootstrapper as Bootstrapper).Style = style;
            return bootstrapper;
        }

        public static IBootstrapper SetMainQml(this IBootstrapper bootstrapper, string mainQml)
        {
            (bootstrapper as Bootstrapper).MainQml = mainQml;
            return bootstrapper;
        }

        public static IBootstrapper SetAttribute(this IBootstrapper bootstrapper, ApplicationAttribute attribute)
        {
            QCoreApplication.SetAttribute(attribute, true);
            return bootstrapper;
        }

        public static IBootstrapper AddImportPath(this IBootstrapper bootstrapper, params string[] importPath)
        {
            if (importPath != null && importPath.Length != 0)
            {
                (bootstrapper as Bootstrapper).ImportPath.AddRange(importPath);
            }
            return bootstrapper;
        }

        public static IBootstrapper RegisterResource(this IBootstrapper bootstrapper, params string[] rccs)
        {
            if (rccs == null)
            {
                return bootstrapper;
            }
            foreach (var rcc in rccs)
            {
                QResource.RegisterResource(rcc);
            }
            return bootstrapper;
        }


        public static IBootstrapper EnableLogging(this IBootstrapper bootstrapper, bool enable = true)
        {
            (bootstrapper as Bootstrapper).EnableLogging = enable;
            return bootstrapper;
        }
    }
}
