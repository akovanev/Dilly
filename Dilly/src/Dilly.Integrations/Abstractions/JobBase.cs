using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dilly.Integrations.Abstractions
{
    public abstract class JobBase<TConsumerProcessor> : BackgroundService
        where TConsumerProcessor : IConsumerProcessor
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public JobBase(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceScopeFactory.CreateScope();

            var consumerProcessor = scope.ServiceProvider.GetRequiredService<TConsumerProcessor>();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(100);
                    string message = consumerProcessor.ReadMessage(stoppingToken);
                    HandleMessage(message);
                }
                catch (Exception exception)
                {
                    HandleError(exception.Message);
                }
            }
        }

        protected abstract void HandleMessage(string message);
        protected abstract void HandleError(string error);
    }
}
