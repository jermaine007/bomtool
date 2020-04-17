using Qml.Net;
using Qml.Net.Runtimes;
using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI.Core
{
    public static class BootstrapperBuilderExtensions
    {
        public static BootstrapperBuilder DetectQtRuntime(this BootstrapperBuilder builder)
        {
            builder.ResolveQtRuntime = () =>
            {
                var loader = new QtRuntimeLoader();
                if (loader.HasBuiltInRuntime)
                {
                    loader.Load();
                }
                else
                {
                    RuntimeManager.DiscoverOrDownloadSuitableQtRuntime();
                }
            };
            return builder;
        }

        public static BootstrapperBuilder RegisterTypes(this BootstrapperBuilder builder, Action doRegisterTypes)
        {
            builder.DoRegisterTypes = doRegisterTypes;
            return builder;
        }

        public static BootstrapperBuilder SetStyle(this BootstrapperBuilder builder, string style = "Materail")
        {
            builder.Style = style;
            return builder;
        }

        public static BootstrapperBuilder SetMainQml(this BootstrapperBuilder builder, string mainQml)
        {
            builder.MainQml = mainQml;
            return builder;
        }

        public static BootstrapperBuilder SetAttribute(this BootstrapperBuilder builder, ApplicationAttribute attribute)
        {
            QCoreApplication.SetAttribute(attribute, true);
            return builder;
        }

        public static BootstrapperBuilder AddImportPath(this BootstrapperBuilder builder, params string[] importPath)
        {
            if (importPath != null && importPath.Length != 0)
            {
                builder.ImportPath.AddRange(importPath);
            }
            return builder;
        }

        public static BootstrapperBuilder RegisterResource(this BootstrapperBuilder builder, params string[] rccs)
        {
            if (rccs == null)
            {
                return builder;
            }
            foreach (var rcc in rccs)
            {
                QResource.RegisterResource(rcc);
            }
            return builder;
        }


        public static BootstrapperBuilder EnableLogging(this BootstrapperBuilder builder, bool enable = true)
        {
            builder.EnableLogging = enable;
            return builder;
        }
    }
}
