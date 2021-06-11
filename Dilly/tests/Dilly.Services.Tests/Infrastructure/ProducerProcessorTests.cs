using System.Threading.Tasks;
using Confluent.Kafka;
using Moq;
using Xunit;
using Dilly.Service.Infrastructure;
using Dilly.Services.Tests.Extensions;

namespace Dilly.Services.Tests.Infrastructure
{
    public class ProducerProcessorTests
    {
        [Fact]
        public async Task PublishMessage_SuccessfulResponse_RetunsSuccess()
        {
            // Setup
            var producerMock = new Mock<IProducer<Null, string>>();
            var deliveryResult = new DeliveryResult<Null, string>();
            deliveryResult.Message = new Message<Null, string>() { Value = "OK" };
            producerMock.SetupProducerMock().ReturnsAsync(deliveryResult);

            // Create
            var producerProcessor = new ProducerProcessor(producerMock.Object);

            // Assert
            var actual = await producerProcessor.PublishMessageAsync("topic", "message");
            Assert.True(actual.IsSuccess);
        }

        [Fact]
        public async Task PublishMessage_ProduceException_RetunsFail()
        {
            // Setup
            var producerMock = new Mock<IProducer<Null, string>>();
            var error = new Error(ErrorCode.Local_BadMsg, "Exception");
            var exception = new ProduceException<Null, string>(error, null);
            producerMock.SetupProducerMock().ThrowsAsync(exception);

            // Create
            var producerProcessor = new ProducerProcessor(producerMock.Object);

            // Assert
            var actual = await producerProcessor.PublishMessageAsync("topic", "message");
            Assert.False(actual.IsSuccess);
        }
    }
}
