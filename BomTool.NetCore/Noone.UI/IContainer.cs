using System;
using System.Collections.Generic;

namespace Noone.UI
{
    public interface IContainer
    {
        /// <summary>
        /// Register a service
        /// </summary>
        /// <param name="service">service type</param>
        /// <param name="singleton">is singleton</param>
        void Register(Type service, bool singleton = false);

        /// <summary>
        /// Register a service
        /// </summary>
        /// <typeparam name="TService">service type</typeparam>
        /// <param name="singleton">is singleton</param>
        void Register<TService>(bool singleton = false);

        /// <summary>
        /// Register a service which related to a specified interface
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="service"></param>
        /// <param name="singleton"></param>
        void Register(Type interfaceType, Type service, bool singleton = false);


        /// <summary>
        /// Register a service which related to a specified interface
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="singleton"></param>
        void Register<TInterface, TImplementation>(bool singleton = false) where TImplementation : TInterface;

        /// <summary>
        /// Register a serivce with an service instance
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceInstance"></param>
        void RegisterConstant<TService>(TService serviceInstance);

        /// <summary>
        /// Register a delegate
        /// </summary>
        /// <typeparam name="TDelegate"></typeparam>
        /// <param name="factory"></param>
        void RegisterMethod<TDelegate>(Func<TDelegate> factory);

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
