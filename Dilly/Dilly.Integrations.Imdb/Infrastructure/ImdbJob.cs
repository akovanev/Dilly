using System;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Dilly.Integrations.Abstractions;
using Dilly.Integrations.Models;

namespace Dilly.Integrations.Imdb.Infrastructure
{
    internal class ImdbJob : JobBase<ImdbConsumerProcessor>
    {
        public ImdbJob(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
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
