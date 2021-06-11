﻿using Confluent.Kafka;
using Dilly.Integrations.Abstractions;

namespace Dilly.Integrations.LetterBoxd.Infrastructure
{
    public class LetterBoxdConsumerProcessor : ConsumerProcessorBase, IConsumerProcessor
    {
        public LetterBoxdConsumerProcessor(ConsumerConfig config)
             : base(new ConsumerBuilder<Ignore, string>(config).Build())
        {
            Consumer.Subscribe("Netflix");
        }
    }
}
