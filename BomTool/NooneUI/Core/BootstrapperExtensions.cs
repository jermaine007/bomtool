using Qml.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI.Core
{
    public static class BootstrapperExtensions
    {
        public static Bootstrapper DetectQtRuntime(this Bootstrapper bootstrapper)
        {
            var loader = new QtRuntimeLoader();
            if (loader.HasBuiltIn)
            {
                bootstrapper.ResolveQtRuntime = loader.LoadBuiltIn;
            }
            return bootstrapper;
        }

        public static Bootstrapper RegisterTypes(this Bootstrapper bootstrapper, Action doRegisterTypes)
        {
            bootstrapper.DoRegisterTypes = doRegisterTypes;
            return bootstrapper;
        }

        public static Bootstrapper SetStyle(this Bootstrapper bootstrapper, string style = "Materail")
        {
            bootstrapper.Style = style;
            return bootstrapper;
        }

        public static Bootstrapper SetMainMainQml(this Bootstrapper bootstrapper, string mainQml)
        {
            bootstrapper.MainQml = mainQml;
            return bootstrapper;
        }

        public static Bootstrapper SetAttribute(this Bootstrapper bootstrapper, ApplicationAttribute attribute)
        {
            QCoreApplication.SetAttribute(attribute, true);
            return bootstrapper;
        }

        public static Bootstrapper AddImportPath(this Bootstrapper bootstrapper, params string[] importPath)
        {
            if (importPath != null && importPath.Length != 0)
            {
                bootstrapper.ImportPath.AddRange(importPath);
            }
            return bootstrapper;
        }

        public static Bootstrapper RegisterResource(this Bootstrapper bootstrapper, params string[] rccs)
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
    }
}
