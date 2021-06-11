using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Confluent.Kafka;
using Dilly.Integrations.Abstractions;
using Dilly.Integrations.LetterBoxd.Infrastructure;

namespace Dilly.Integrations.LetterBoxd
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
                        services.AddScoped<LetterBoxdConsumerProcessor>();
                        services.AddHostedService<LetterBoxdJob>();
                    })
                    .UseConsoleLifetime()
                    .Build();

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Clear();
                Console.WriteLine($"Welcome to LetterBoxd{Environment.NewLine}");
                await host.RunAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"**Smth went wrong** {exception.Message}");
            }
        }
    }
}
