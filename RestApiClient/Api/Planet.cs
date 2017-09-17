namespace Astroants.RestApiClient.Api
{
    public class Planet
    {
        public ulong Id { get; set; }
        public ulong StartedTimestamp { get; set; }
        public Map Map { get; set; }
        public Point Astroants { get; set; }
        public Point Sugar { get; set; }
    }
}