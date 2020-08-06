using Ninject;
using Ninject.Planning.Bindings;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NooneUI.Framework
{
    /// <summary>
    /// Simple IoC container based on Ninject. Singleton.
    /// </summary>
    internal class LightContainer : IContainer
    {
        /// <summary>
        /// Single instance
        /// </summary>
        public static readonly LightContainer Instance;

        static LightContainer() => Instance = new LightContainer();

        /// <summary>
        /// Core container used for register sevices
        /// </summary>
        public IKernel Kernel { get; private set; }

        private LightContainer()
        {
            var settings = new NinjectSettings { LoadExtensions = false };
            Kernel = new StandardKernel(settings, new LightMoudle());
        }

        /// <summary>
        /// Register a type
        /// </summary>
        /// <param name="service"></param>
        /// <param name="singleton"></param>
        public void Bind(Type service, bool singleton)
        {

            if (singleton)
            {
                IEnumerable<IBinding> bindings = Kernel.GetBindings(service);

                IBindingWhenInNamedWithOrOnSyntax<object> binding = bindings.Count() == 0 ? Kernel.Bind(service).ToSelf() : Kernel.Rebind(service).ToSelf();

                binding.InSingletonScope();
            }
            else
            {
                Kernel.Bind(service).ToSelf();
            }
        }

        public void Bind<TInterface, TImplementation>(bool singleton) where TImplementation : TInterface
        {
            //https://github.com/ninject/Ninject/issues/243
            if (singleton)
            {
                Kernel.Bind<TImplementation, TInterface>().To<TImplementation>().InSingletonScope();
            }
            else
            {
                Kernel.Bind<TInterface>().To<TImplementation>();
            }
        }

        public void Bind<TService>(bool singleton)
        {
            if (singleton)
            {
                IEnumerable<IBinding> bindings = Kernel.GetBindings(typeof(TService));

                IBindingWhenInNamedWithOrOnSyntax<TService> binding = bindings.Count() == 0 ? Kernel.Bind<TService>().ToSelf() : Kernel.Rebind<TService>().ToSelf();

                binding.InSingletonScope();
            }
            else
            {
                Kernel.Bind<TService>().ToSelf();
            }
        }

        public void BindConstant<TService>(TService serviceInstance)
        {
            Kernel.Bind<TService>().ToConstant(serviceInstance);
        }

        public void BindMethod<TDelegate>(Func<TDelegate> factory) => Kernel.Bind<TDelegate>().ToMethod(context => factory());

        public TService Get<TService>() => Kernel.Get<TService>();

        public IEnumerable<TService> GetAll<TService>() => Kernel.GetAll<TService>();

        public object Get(Type service) => Kernel.Get(service);

        public IEnumerable<object> GetAll(Type service) => Kernel.GetAll(service);

    }
}