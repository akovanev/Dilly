using Confluent.Kafka;
using Dilly.Integrations.Abstractions;

namespace Dilly.Integrations.Imdb.Infrastructure
{
    internal class ImdbConsumerProcessor : ConsumerProcessorBase, IConsumerProcessor
    {
        public ImdbConsumerProcessor(ConsumerConfig config)
            : base(new ConsumerBuilder<Ignore, string>(config).Build())
        {
            Consumer.Subscribe(new string[] { "Disney", "Netflix" });
        }
    }
}
