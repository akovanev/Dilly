using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Dilly.Integrations.Abstractions
{
    public abstract class JobBase : BackgroundService
    {
        private readonly IConsumerProcessor consumerProcessor;

        public JobBase(IConsumerProcessor consumerProcessor)
        {
            this.consumerProcessor = consumerProcessor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                TryGetMessage(stoppingToken);
            }

            //As ExecuteAsync requires Task, await will respond with it.
            //Delay(0) should make an asynchronious run synchronious at this point
            //https://akovanev.com/blogs/2020/07/29/when-async-code-becomes-sync
            await Task.Delay(0);
        }

        protected internal virtual void TryGetMessage(CancellationToken stoppingToken)
        {
            try
            {
                string? message = consumerProcessor.ReadMessage(stoppingToken);

                if (message is not null)
                {
                    HandleMessage(message);
                }
            }
            catch (Exception exception)
            {
                HandleError(exception.Message);
            }
        }

        protected internal abstract void HandleMessage(string message);
        protected internal abstract void HandleError(string error);
    }
}
