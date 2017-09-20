using Astroants.Core;
using Astroants.RestApiClient.Api;
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
                BaseAddress = new Uri(url),
                Timeout = TimeSpan.FromSeconds(15)
            };
        }

        public async Task<Planet> GetAsync()
        {
            try
            {
                var response = await _client.GetStringAsync("task");
                var planetJson = JsonConvert.DeserializeObject<PlanetJson>(response);
#if DEBUG
                await WriteDebugFile(planetJson.Id, "planet", response);
#endif
                return planetJson.ToPlanet();
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Connection timeout.");
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<ResultJson> PutAsync(ulong planetId, List<Direction> path)
        {
            HttpResponseMessage response = null;
            try
            {
                var json = $"{{\"path\":\"{path.GetCodes()}\"}}";
#if DEBUG
                await WriteDebugFile(planetId, "solution", json);
#endif
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await _client.PutAsync($"task/{planetId}", content);
                
                if (!response.IsSuccessStatusCode)
                    return null;

                json = await response.Content.ReadAsStringAsync();
#if DEBUG
                await WriteDebugFile(planetId, "result", json);
#endif
                return JsonConvert.DeserializeObject<Api.ResultJson>(json);
            }
            catch(TaskCanceledException)
            {
                Console.WriteLine("Connection timeout.");
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                response?.Dispose();
            }
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
