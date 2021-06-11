using Confluent.Kafka;
using Dilly.Service.Abstractions;
using Dilly.Service.Models;
using System.Threading.Tasks;

namespace Dilly.Service.Infrastructure
{
    public class ProducerProcessor : IProducerProcessor
    {
        private readonly ProducerConfig config;

        public ProducerProcessor(ProducerConfig config)
        {
            this.config = config;
        }

        public async Task<Result<string>> PublishMessage(string topic, string message)
        {
            using var producerBuilder = new ProducerBuilder<Null, string>(config).Build();

            try
            {
                var dr = await producerBuilder.ProduceAsync(topic, new Message<Null, string> { Value = message });
                return Result<string>.Success($"Delivered '{dr.Value}'");
            }
            catch (ProduceException<Null, string> exception)
            {
                return Result<string>.Fail($"Delivery failed: {exception.Error.Reason}", exception);
            }
        }
    }
}
