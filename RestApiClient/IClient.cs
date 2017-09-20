using Astroants.Core;
using Astroants.RestApiClient.Api;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Astroants.RestApiClient
{
    public interface IClient
    {
        Task<Planet> GetAsync();
        Task<ResultJson> PutAsync(ulong planetId, List<Direction> path);
    }
}
