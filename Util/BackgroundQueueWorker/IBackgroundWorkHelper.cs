using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace dytsenayasar.Util.BackgroundQueueWorker
{
    public interface IBackgroundWorkHelper
    {
        void Queue(Func<IServiceScope, CancellationToken, Task> work);
        Task<Func<IServiceScope, CancellationToken, Task>> Dequeue(CancellationToken cancellationToken);
    }
}