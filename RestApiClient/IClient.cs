using Astroants.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Astroants.RestApiClient
{
    public interface IClient
    {
        Task<Planet> GetAsync();
        Task<Api.Result> PutAsync(ulong planetId, List<Direction> path);
    }
}
