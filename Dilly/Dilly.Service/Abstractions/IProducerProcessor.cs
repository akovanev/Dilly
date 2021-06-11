using System.Threading.Tasks;
using Dilly.Service.Models;

namespace Dilly.Service.Abstractions
{
    public interface IProducerProcessor
    {
        Task<Result<string>> PublishMessage(string topic, string message);
    }
}
