using NooneUI.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BomTool.Models
{
    public abstract class Dispatchable
    {
        protected void Dispatch(Action action) => Bootstrapper.Application.Dispatch(action);
    }
}
