using System;
using Priority_Queue;

namespace Astroants.Core
{
    public class Area : FastPriorityQueueNode 
    {
        public Coords Coords { get; set; }
        public int Duration { get; set; }
        public Direction[] Neighbors { get; set; }

        internal bool Visited { get; set; }
        internal Direction CameFrom { get; set; }
        internal int DurationFromStart { get; set; }
        internal int EstimatedDurationToEnd { get; private set; }
        internal int TotalEstimatedDuration => DurationFromStart + EstimatedDurationToEnd;

        public Area()
        {
            DurationFromStart = int.MaxValue;
        }

        public void SetEstimatedDurationTo(Coords endArea)
        {
            EstimatedDurationToEnd = Math.Abs(Coords.X - endArea.X) + Math.Abs(Coords.Y - endArea.Y);
        }
    }
}
