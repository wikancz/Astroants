using Astroants.Core;
using Astroants.RestApiClient;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Astroants.Runner
{
    class Program
    {
        const string Url = "http://tasks-rad.quadient.com:8080";

        static async Task Main(string[] args)
        {
            using (var client = new Client(Url))
            {
                var ok = await Run(client);
                if (!ok)
                    Console.WriteLine("Error occured.");
            }
        }

        static async Task<bool> Run(IClient client)
        {
            var watch = new Stopwatch();


            Console.Write("Fetching planet...");
            watch.Start();
            var planet = await client.GetAsync();
            WriteDone(watch);
            if (planet == null)
                return false;

            Console.Write("Finding path...");
            watch.Start();
            var solution = Solver.Solve(planet);
            WriteDone(watch);
            if (solution == null)
                return false;

            Console.Write("Sending result...");
            watch.Start();
            var result = await client.PutAsync(planet.Id, solution.Path);
            WriteDone(watch);
            if (result == null)
                return false;

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
            return true;
        }

        static void WriteDone(Stopwatch watch)
        {
            Console.WriteLine($" done in {watch.ElapsedMilliseconds} ms");
            watch.Reset();
        }
    }
}
