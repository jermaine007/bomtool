using System;
using System.Collections.Generic;
using System.Text;

namespace NooneUI.Framework
{
    public interface IContainer
    {
        /// <summary>
        /// Register a service
        /// </summary>
        /// <param name="service">service type</param>
        /// <param name="singleton">is singleton</param>
        void Bind(Type service, bool singleton = false);

        /// <summary>
        /// Register a service
        /// </summary>
        /// <typeparam name="TService">service type</typeparam>
        /// <param name="singleton">is singleton</param>
        void Bind<TService>(bool singleton = false);

        /// <summary>
        /// Register a service which related to a specified interface
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="singleton"></param>
        void Bind<TInterface, TImplementation>(bool singleton = false) where TImplementation : TInterface;

        /// <summary>
        /// Register a serivce with an service instance
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceInstance"></param>
        void BindConstant<TService>(TService serviceInstance);

        /// <summary>
        /// Register a delegate
        /// </summary>
        /// <typeparam name="TDelegate"></typeparam>
        /// <param name="factory"></param>
        void BindMethod<TDelegate>(Func<TDelegate> factory);
       
        /// <summary>
        /// Get a service instance
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        object Get(Type service);

        /// <summary>
        /// Get a service instance
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        TService Get<TService>();

        /// <summary>
        /// Get all service instances
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        IEnumerable<object> GetAll(Type service);

        /// <summary>
        /// Get all service instances
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        IEnumerable<TService> GetAll<TService>();
    }
}
