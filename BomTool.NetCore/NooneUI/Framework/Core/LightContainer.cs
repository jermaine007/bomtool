using Ninject;
using Ninject.Planning.Bindings;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NooneUI.Framework
{
    /// <summary>
    /// Simple IoC container based on Ninject. Singleton instace
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
            //var settings = new NinjectSettings { LoadExtensions = false };
            //Kernel = new StandardKernel(settings, new LightMoudle());
            Kernel = new StandardKernel(new NinjectSettings { LoadExtensions = false });
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


        /// <summary>
        /// Register a service which related to a specified interface
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="service"></param>
        /// <param name="singleton"></param>
        public void Bind(Type interfaceType, Type service, bool singleton)
        {
            if (!interfaceType.IsAssignableFrom(service))
            {
                throw new InvalidOperationException($"{interfaceType} must be assignable from {service}");
            }

            IBindingWhenInNamedWithOrOnSyntax<object> binding = Kernel.Bind(interfaceType).To(service);

            if (singleton)
            {
                binding.InSingletonScope();
            }

        }

        /// <summary>
        /// Register a type which is implement a specified interface
        /// </summary>
        /// <param name="singleton">is singleton</param>
        /// <typeparam name="TInterface">interface type</typeparam>
        /// <typeparam name="TImplementation">implementation type</typeparam>
        /// <returns></returns>
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

        /// <summary>
        ///  Register a type
        /// </summary>
        /// <param name="singleton">is singleton</param>
        /// <typeparam name="TService">Service type which need to be registered</typeparam>
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

        /// <summary>
        /// Register a type instance
        /// </summary>
        /// <param name="serviceInstance"></param>
        /// <typeparam name="TService"></typeparam>
        public void BindConstant<TService>(TService serviceInstance) => Kernel.Bind<TService>().ToConstant(serviceInstance);

        /// <summary>
        /// Register a delegate instance
        /// </summary>
        /// <param name="factory"></param>
        /// <typeparam name="TDelegate"></typeparam>
        public void BindMethod<TDelegate>(Func<TDelegate> factory) => Kernel.Bind<TDelegate>().ToMethod(context => factory());

        /// <summary>
        /// Get a specified service instance
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public TService Get<TService>() => Kernel.Get<TService>();

        /// <summary>
        /// Get the registered spcified type's all instances
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public IEnumerable<TService> GetAll<TService>() => Kernel.GetAll<TService>();

        /// <summary>
        /// Get a specified service instance
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public object Get(Type service) => Kernel.Get(service);

        /// <summary>
        /// Get a specified service instance
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public IEnumerable<object> GetAll(Type service) => Kernel.GetAll(service);

    }
}
