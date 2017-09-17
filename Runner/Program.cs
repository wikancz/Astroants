using Astroants.Core;
using Astroants.RestApiClient;
using System;
using System.Threading.Tasks;

namespace Astroants.Runner
{
    class Program
    {
        const string Url = "http://tasks-rad.quadient.com:8080";

        static int Main(string[] args)
        {
            using (var client = new Client(Url))
            {
                return Run(client).Result;
            }
        }

        static async Task<int> Run(IClient client)
        {
            var planet = await client.GetAsync();
            if (planet == null)
                return 1;

            var solution = Solver.Solve(planet);

            var result = await client.PutAsync(planet.Id, solution.Path);
            if (result == null)
                return 1;

            if (result.Valid)
            {
                var time = result.InTime ? "OK" : "Slow";
                Console.WriteLine($"Valid: {time}");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
#if DEBUG
            Console.ReadKey();
#endif
            return 0;
        }
    }
}
