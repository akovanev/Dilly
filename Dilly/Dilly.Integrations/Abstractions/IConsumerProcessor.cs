using System.Threading;

namespace Dilly.Integrations.Abstractions
{
    public interface IConsumerProcessor
    {
        string ReadMessage(CancellationToken cancellationToken);
    }
}
