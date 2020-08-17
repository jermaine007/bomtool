using System;
using System.Collections.Generic;
using System.Text;
using Noone.UI.Core;

namespace Noone.UI
{
    public interface ITemplateEngineProvider
    {
        ITemplateEngine TemplateEngine => ContainerLocator.Current.Get<ITemplateEngine>();
    }
}
