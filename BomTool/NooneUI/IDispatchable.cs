using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NooneUI
{
    public interface IDispatchable
    {
        void Dispatch(Action action);
        Task DispatchAsync(Func<Task> action);
    }
}
