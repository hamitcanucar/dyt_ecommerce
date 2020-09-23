using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace dytsenayasar.Util.BackgroundQueueWorker
{
    public class BackgroundWorker : BackgroundService
    {
        private readonly IBackgroundWorkHelper _workHelper;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public BackgroundWorker(IBackgroundWorkHelper workHelper, IServiceProvider serviceProvider, ILogger<BackgroundService> logger)
        {
            _workHelper = workHelper;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var work = await _workHelper.Dequeue(stoppingToken);

                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        await work(scope, stoppingToken);
                    }
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, "Error occurred executing {0}.", nameof(work));
                }
            }
        }
    }
}