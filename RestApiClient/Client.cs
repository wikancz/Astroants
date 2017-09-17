using Astroants.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Astroants.RestApiClient
{
    public class Client : IClient, IDisposable
    {
        readonly HttpClient _client;

        public Client(string url)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
        }

        public async Task<Planet> GetAsync()
        {
            var response = await _client.GetAsync("task");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var planet = JsonConvert.DeserializeObject<Api.Planet>(json);
#if DEBUG
            await WriteDebugFile(planet.Id, "planet", json);
#endif
            return PlanetFactory.Create(planet);
        }

        public async Task<Api.Result> PutAsync(ulong planetId, List<Direction> path)
        {
            var json = $"{{\"path\":\"{path.GetCodes()}\"}}";
#if DEBUG
            await WriteDebugFile(planetId, "solution", json);
#endif
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"task/{planetId}", content);
            if (!response.IsSuccessStatusCode)
                return null;

            json = await response.Content.ReadAsStringAsync();
#if DEBUG
            await WriteDebugFile(planetId, "result", json);
#endif
            return JsonConvert.DeserializeObject<Api.Result>(json);
        }

        public void Dispose()
        {
            _client?.Dispose();
        }

#if DEBUG
        static async Task WriteDebugFile(ulong planetId, string fileName, string content)
        {
            var directory = $"planet-{planetId}";
            Directory.CreateDirectory(directory);
            await File.WriteAllTextAsync($"{directory}/{fileName}.json", content);
        }
#endif
    }
}
