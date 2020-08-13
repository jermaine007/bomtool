using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI.Framework
{
    public interface ITemplateEngineProvider
    {
        ITemplateEngine TemplateEngine => ContainerLocator.Current.Get<ITemplateEngine>();
    }
}
