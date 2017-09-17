using System.Collections.Generic;

namespace Astroants.Core
{
    public class Area
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Duration { get; set; }

        public Dictionary<Direction, Area> Neighbors { get; set; }

        public bool Visited { get; set; }
        public bool InQueue { get; set; }
        public Direction CameFrom { get; set; }
        public int TotalDuration { get; set; }

        public Area()
        {
            Neighbors = new Dictionary<Direction, Area>();
            TotalDuration = int.MaxValue;
        }
    }
}
