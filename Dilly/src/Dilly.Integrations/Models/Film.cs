using System;

namespace Dilly.Integrations.Models
{
    public class Film
    {
        public Film()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public string? FilmMakerId { get; set; }
        public string? Name { get; set; }
        public string? Actors { get; set; }
        public DateTime CreatedAt { get; }
    }
}
