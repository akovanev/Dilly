using System;
using System.Threading;
using Confluent.Kafka;

namespace Dilly.Integrations.Abstractions
{
    public abstract class ConsumerProcessorBase
    {
        protected readonly IConsumer<Ignore, string> Consumer;

        public ConsumerProcessorBase(IConsumer<Ignore, string> consumer)
        {
            Consumer = consumer ?? throw new NullReferenceException(nameof(consumer));
        }

        public string? ReadMessage(CancellationToken cancellationToken)
        {
            var consumeResult = Consumer.Consume(cancellationToken);
            return consumeResult.Message?.Value;
        }
    }
}
