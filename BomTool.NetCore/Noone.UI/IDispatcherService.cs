using System;
using System.Threading.Tasks;

namespace Noone.UI
{
    public interface IDispatcherService
    {
        public Task InvokeAsync(Action action);
        public Task<TResult> InvokeAsync<TResult>(Func<TResult> function);
        public Task InvokeAsync(Func<Task> function);
        public Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> function);
    }
}
