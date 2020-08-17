using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Noone.UI.Core
{
    [AutoRegister(Singleton = true, InterfaceType = typeof(IDispatcherService))]
    internal class LightDispatcherService : IDispatcherService, ILoggerProvider
    {
        private readonly ILogger logger;

        public LightDispatcherService()
        {
            logger = ((ILoggerProvider)this).Logger;
        }

        public Task InvokeAsync(Action action) => Dispatcher.UIThread.InvokeAsync(() =>
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                logger.Error("Execute action error:", ex);
            }
        });


        public Task<TResult> InvokeAsync<TResult>(Func<TResult> function) => Dispatcher.UIThread.InvokeAsync(() =>
        {
            try
            {
                return function();
            }
            catch (Exception ex)
            {
                logger.Error("Execute action error:", ex);
            }
            return default;
        });



        public Task InvokeAsync(Func<Task> function) => Dispatcher.UIThread.InvokeAsync(() =>
        {
            try
            {
                return function();
            }
            catch (Exception ex)
            {
                logger.Error("Execute action error:", ex);
            }
            return null;
        });

        public Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> function) => Dispatcher.UIThread.InvokeAsync(() =>
        {
            try
            {
                return function();
            }
            catch (Exception ex)
            {
                logger.Error("Execute action error:", ex);
            }
            return null;
        });
    }
}
