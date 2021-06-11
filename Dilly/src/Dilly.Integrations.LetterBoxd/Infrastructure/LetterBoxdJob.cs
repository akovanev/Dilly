using System;
using Newtonsoft.Json;
using Dilly.Integrations.Abstractions;
using Dilly.Integrations.Models;

namespace Dilly.Integrations.LetterBoxd.Infrastructure
{
    internal class LetterBoxdJob : JobBase
    {
        public LetterBoxdJob(IConsumerProcessor consumerProcessor)
             : base(consumerProcessor)
        {
        }

        protected override void HandleMessage(string message)
        {
            var film = JsonConvert.DeserializeObject<Film>(message);
            Console.WriteLine(film.Name);
        }

        protected override void HandleError(string error)
        {
            Console.WriteLine($"Smth went wrong. {error}");
        }
    }
}
