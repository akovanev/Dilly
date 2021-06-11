using System.Threading.Tasks;
using Confluent.Kafka;
using Dilly.Service.Abstractions;
using Dilly.Service.Models;

namespace Dilly.Service.Infrastructure
{
    public class ProducerProcessor : IProducerProcessor
    {
        private readonly IProducer<Null, string> producer;

        public ProducerProcessor(IProducer<Null, string> producer)
        {
            this.producer = producer;
        }

        public async Task<Result<string>> PublishMessageAsync(string topic, string message)
        {
            try
            {
                var dr = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
                return Result<string>.Success($"Delivered '{dr.Value}'");
            }
            catch (ProduceException<Null, string> exception)
            {
                return Result<string>.Fail($"Delivery failed: {exception.Error.Reason}", exception);
            }
        }
    }
}
