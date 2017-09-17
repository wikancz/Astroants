using System;

namespace Astroants.Core
{
    public class Planet
    {
        public ulong Id { get; set; }

        public Area[,] Areas { get; set; }
        public Area Astroants { get; set; }
        public Area Sugar { get; set; }
    }
}
