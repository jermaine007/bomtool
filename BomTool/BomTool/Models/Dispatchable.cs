using NooneUI;
using Qml.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace BomTool.Models
{
    public abstract class Dispatchable : Loggable
    {
        protected void Dispatch(Action action) => this.Container.Get<IBootstrapper>().Application.Dispatch(action);

        protected bool IsMainThread => QCoreApplication.IsMainThread;
    }
}
