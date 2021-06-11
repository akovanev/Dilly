using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Moq;
using Moq.Language.Flow;

namespace Dilly.Services.Tests.Extensions
{
    internal static class ProducerMockExtensions
    {
        public static ISetup<IProducer<TKey, TValue>, Task<DeliveryResult<TKey, TValue>>> SetupProducerMock<TKey, TValue>(this Mock<IProducer<TKey, TValue>> mock)
            => mock.Setup(m => m.ProduceAsync(It.IsAny<string>(), It.IsAny<Message<TKey, TValue>>(), It.IsAny<CancellationToken>()));
    }
}
