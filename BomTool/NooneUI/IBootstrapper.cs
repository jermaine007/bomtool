using NooneUI.Services;
using Qml.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI
{
    public interface IBootstrapper 
    {
        string ApplicationDirectory { get; }

        ServicesContainer ServicesContainer { get; }

        int Launch(string[] args = null);
    }
}
