using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Disney
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new ApiClient();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Clear();
            Console.WriteLine($"Welcome to NETFLIX{Environment.NewLine}");


            while (true)
            {
                Console.WriteLine("Input command: New or Exit");
                var input = Console.ReadLine();

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                if (input.Equals("new", StringComparison.OrdinalIgnoreCase))
                {
                    var film = new Film();
                    Console.Write("Name >> ");
                    film.Name = Console.ReadLine();
                    Console.Write("Actors >> ");
                    film.Actors = Console.ReadLine();
                    var result = await client.PostAsync(film);
                    Console.WriteLine($"{result}{Environment.NewLine}");
                }
            }
        }
    }

    class Film
    {
        //For simplicity
        public string FilmMakerId => "Netflix";
        public string Name { get; set; }
        public string Actors { get; set; }
    }

    class ApiClient
    {
        private static readonly HttpClient client = new();
        private const string Url = "http://localhost:7000/film";

        public async Task<string> PostAsync(Film film)
        {
            try
            {
                var response = await client.PostAsJsonAsync(Url, film);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch(Exception ex)
            {
                return $"**Smth went wrong** {ex.Message}";
            }
        }
    }
}
