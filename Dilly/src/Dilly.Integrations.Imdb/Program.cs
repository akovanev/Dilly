using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Confluent.Kafka;
using Dilly.Integrations.Abstractions;
using Dilly.Integrations.Imdb.Infrastructure;

namespace Dilly.Integrations.Imdb
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var host = new HostBuilder()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .ConfigureHostConfiguration(configurationBuilder => {
                        configurationBuilder.AddJsonFile("appsettings.json", false);
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddSingleton(svc => hostContext.Configuration.GetSection("KafkaConsumerCongig").Get<ConsumerConfig>());
                        services.AddScoped<IConsumerProcessor, ImdbConsumerProcessor>();
                        services.AddHostedService<ImdbJob>();
                    })
                    .UseConsoleLifetime()
                    .Build();

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Clear();
                Console.WriteLine($"Welcome to IMDB{Environment.NewLine}");
                await host.RunAsync();
            }
            catch(Exception exception)
            {
                Console.WriteLine($"**Smth went wrong** {exception.Message}");
            }
        }
    }
}
