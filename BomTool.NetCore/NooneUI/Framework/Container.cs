using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;

namespace NooneUI.Framework
{
    public class Container
    {
        public static readonly Container Current;

        static Container() => Current = new Container();

        public IKernel Kernel { get; private set; }

        private Container()
        {
            var settings = new NinjectSettings { LoadExtensions = false };
            Kernel = new StandardKernel(settings, new LightMoudle());
        }

        public void Bind(Type type) => Kernel.Bind(type).ToSelf();

        public void Bind<TInterface, TImplementation>(bool isSingletone = false) where TImplementation : TInterface
        {
            //https://github.com/ninject/Ninject/issues/243
            if (isSingletone)
            {
                Kernel.Bind<TImplementation, TInterface>().To<TImplementation>().InSingletonScope();
            }
            else
            {
                Kernel.Bind<TInterface>().To<TImplementation>();
            }
        }

        public void Bind<TClass>(bool isSingletone = false)
        {
            var bindings = Kernel.GetBindings(typeof(TClass));

            var o = bindings.Count() == 0 ? Kernel.Bind<TClass>().ToSelf() : Kernel.Rebind<TClass>().ToSelf();

            if (isSingletone)
            {
                o.InSingletonScope();
            }
        }

        public void Bind<T>(T o)
        {
            Kernel.Bind<T>().ToConstant(o);
        }

        public void BindToMethod<TDelegate>(Func<TDelegate> func) => Kernel.Bind<TDelegate>().ToMethod(context => func());

        public T Get<T>() => Kernel.Get<T>();

        public IEnumerable<T> GetAll<T>() => Kernel.GetAll<T>();

        public object Get(Type type) => Kernel.Get(type);

    }
}