using NooneUI;
using Qml.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace BomTool.Models
{
    /// <summary>
    /// Class provides dispatch sevices to allow operation from non ui thread
    /// </summary>
    public abstract class Dispatchable : Loggable
    {
        protected void Dispatch(Action action) => this.Container.Get<IBootstrapper>().Application.Dispatch(action);

        protected bool IsMainThread => QCoreApplication.IsMainThread;
    }
}
