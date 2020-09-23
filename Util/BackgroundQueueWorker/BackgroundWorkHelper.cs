using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace dytsenayasar.Util.BackgroundQueueWorker
{
    public class BackgroundWorkHelper : IBackgroundWorkHelper
    {
        private Queue<Func<IServiceScope, CancellationToken, Task>> _workStack = new Queue<Func<IServiceScope, CancellationToken, Task>>();
        private SemaphoreSlim _signal = new SemaphoreSlim(0);

        public void Queue(Func<IServiceScope, CancellationToken, Task> work)
        {
            if (work == null) return;
            _workStack.Enqueue(work);
            _signal.Release();
        }

        public async Task<Func<IServiceScope, CancellationToken, Task>> Dequeue(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            return _workStack.Dequeue();
        }
    }
}