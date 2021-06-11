using System;
using Newtonsoft.Json;
using Dilly.Integrations.Abstractions;
using Dilly.Integrations.Models;

namespace Dilly.Integrations.Imdb.Infrastructure
{
    internal class ImdbJob : JobBase
    {
        public ImdbJob(IConsumerProcessor consumerProcessor)
            : base(consumerProcessor)
        {
        }

        protected override void HandleMessage(string message)
        {
            var film = JsonConvert.DeserializeObject<Film>(message);
            Console.WriteLine($"{film.Name} created {film.CreatedAt:HHmmss}");
        }

        protected override void HandleError(string error)
        {
            Console.WriteLine($"Smth went wrong. {error}"); 
        }
    }
}
