using Astroants.Core;

namespace Astroants.RestApiClient.Api
{
    public class PlanetJson
    {
        public ulong Id { get; set; }
        public ulong StartedTimestamp { get; set; }
        public Map Map { get; set; }
        public Coords Astroants { get; set; }
        public Coords Sugar { get; set; }

        public Planet ToPlanet()
        {
            return new Planet
            {
                Id = Id,
                Areas = new LazyAreaArray(Map.Areas),
                Sugar = Sugar,
                Astroants = Astroants
            };
        }
    }
}