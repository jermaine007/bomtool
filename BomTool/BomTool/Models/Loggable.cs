using NooneUI;
using NooneUI.Logging;
using NooneUI.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BomTool.Models
{
    abstract public class Loggable
    {
        public ServicesContainer Container => ServicesContainer.Instance;
        public ILogger Logger => Container.Get<ILogger>();
    }
}
