using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using Ninject.Planning.Bindings;
using Ninject.Syntax;

namespace NooneUI.Framework
{
    public class LightContainer
    {
        public static readonly LightContainer Current;

        static LightContainer() => Current = new LightContainer();

        public IKernel Kernel { get; private set; }

        private LightContainer()
        {
            var settings = new NinjectSettings { LoadExtensions = false };
            Kernel = new StandardKernel(settings, new LightMoudle());
        }

        public void Bind(Type type, bool singleton = false)
        {
            IEnumerable<IBinding> bindings = Kernel.GetBindings(type);

            IBindingWhenInNamedWithOrOnSyntax<object> binding = bindings.Count() == 0 ? Kernel.Bind(type).ToSelf() : Kernel.Rebind(type).ToSelf();

            if (singleton)
            {
                binding.InSingletonScope();
            }
        }

        public void Bind<TInterface, TImplementation>(bool singleton = false) where TImplementation : TInterface
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

        public void Bind<TService>(bool singleton = false)
        {
            IEnumerable<IBinding> bindings = Kernel.GetBindings(typeof(TService));

            IBindingWhenInNamedWithOrOnSyntax<TService> binding = bindings.Count() == 0 ? Kernel.Bind<TService>().ToSelf() : Kernel.Rebind<TService>().ToSelf();

            if (singleton)
            {
                binding.InSingletonScope();
            }
        }

        public void BindToConstant<TService>(TService serviceInstance)
        {
            Kernel.Bind<TService>().ToConstant(serviceInstance);
        }

        public void BindToMethod<TDelegate>(Func<TDelegate> factory) => Kernel.Bind<TDelegate>().ToMethod(context => factory());

        public TService Get<TService>() => Kernel.Get<TService>();

        public IEnumerable<TService> GetAll<TService>() => Kernel.GetAll<TService>();

        public object Get(Type service) => Kernel.Get(service);

        public IEnumerable<object> GetAll(Type service) => Kernel.GetAll(service);

    }
}