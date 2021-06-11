using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Dilly.Integrations.Abstractions;

namespace Dilly.Integrations.Tests.Abstractions
{
    public class JobBaseTests
    {
        [Fact]
        public async Task TryGetMessage_ValidMessage_VerifiesHandleMessage()
        {
            // Setup
            var consumerProcessorMock = new Mock<IConsumerProcessor>();
            consumerProcessorMock.Setup(m => m.ReadMessage(It.IsAny<CancellationToken>())).Returns("OK");

            // Create
            var jobMock = CreateJobBaseMock(consumerProcessorMock.Object);

            // Execute
            await jobMock.Object.StartAsync(new CancellationToken());
            jobMock.Verify(m => m.TryGetMessage(It.IsAny<CancellationToken>()), Times.Once);
            jobMock.Verify(m => m.HandleMessage(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task TryGetMessage_Exception_VerifiesHandleError()
        {
            // Setup
            var consumerProcessorMock = new Mock<IConsumerProcessor>();
            consumerProcessorMock.Setup(m => m.ReadMessage(It.IsAny<CancellationToken>())).Throws<Exception>();

            // Create
            var jobMock = CreateJobBaseMock(consumerProcessorMock.Object);

            // Execute
            await jobMock.Object.StartAsync(new CancellationToken());
            jobMock.Verify(m => m.TryGetMessage(It.IsAny<CancellationToken>()), Times.Once);
            jobMock.Verify(m => m.HandleError(It.IsAny<string>()), Times.Once);
        }

        private static Mock<JobBase> CreateJobBaseMock(IConsumerProcessor consumerProcessor)
        {
            // Create partial mock
            var jobMock = new Mock<JobBase>(consumerProcessor)
            {
                CallBase = true
            };
            jobMock.Setup(m => m.HandleMessage(It.IsAny<string>())).Callback(() => jobMock.Object.Dispose()).Verifiable();
            jobMock.Setup(m => m.HandleError(It.IsAny<string>())).Callback(() => jobMock.Object.Dispose()).Verifiable();
            return jobMock;
        }
    }
}
