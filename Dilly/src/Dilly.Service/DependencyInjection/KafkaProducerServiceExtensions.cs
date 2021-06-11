using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Confluent.Kafka;

namespace Dilly.Service.DependencyInjection
{
    public static class KafkaProducerServiceExtensions
    {
        public static void AddProducer<TKey, TValue>(this IServiceCollection services, IConfiguration configuration)
        {
            var producerConfig = new ProducerConfig();
            configuration.GetSection("KafkaProducerCongig").Bind(producerConfig);
            producerConfig.ClientId = Environment.MachineName;

            var producerBuilder = new ProducerBuilder<TKey, TValue>(producerConfig);
            services.AddSingleton(producerBuilder.Build());
        }
    }
}
