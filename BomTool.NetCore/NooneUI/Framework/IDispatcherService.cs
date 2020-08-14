using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NooneUI.Framework
{
    public interface IDispatcherService
    {
        public Task InvokeAsync(Action action);
        public Task<TResult> InvokeAsync<TResult>(Func<TResult> function);
        public Task InvokeAsync(Func<Task> function);
        public Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> function);
    }
}
