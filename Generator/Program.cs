using Astroants.Core;
using Astroants.RestApiClient.Api;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
                return;

            var rnd = new Random();
            var size = int.Parse(args[0]);
            var sizeSqr = size * size;
            var planet = new PlanetJson
            {
                Areas = new string[sizeSqr],
                Astroants = new Coords(0, 0),
                Sugar = new Coords(size - 1, size - 1)
            };

            for (var x = 0; x < size; x++)
            {
                string left, up, down, right;

                left = x == 0 ? "" : "L";
                right = x == size - 1 ? "" : "R";

                for (var y = 0; y < size; y++)
                {
                    up = y == 0 ? "" : "U";
                    down = y == size - 1 ? "" : "D";

                    planet.Areas[x + y * size] = $"{rnd.Next(9) + 1}-{left}{up}{down}{right}";
                }
            }

            var json = JsonConvert.SerializeObject(planet);
            File.WriteAllText($"test-{size}.json", json);
        }
    }
}
