using System;
using System.Collections.Generic;
using Priority_Queue;

namespace Astroants.Core
{
    public static class Solver
    {
        public static Solution Solve(Planet planet)
        {
            var areaQueue = new SimplePriorityQueue<Area, int>();
            areaQueue.Enqueue(planet.Sugar, GetQueuePriority(planet.Sugar, planet.Astroants));
            planet.Sugar.TotalDuration = 0;

            while (areaQueue.Count > 0)
            {
                var currentArea = areaQueue.Dequeue();
                if (currentArea == planet.Astroants)
                    return GetSolution(planet);

                currentArea.Visited = true;

                foreach (var pair in currentArea.Neighbors)
                {
                    var neighbor = pair.Value;
                    if (neighbor.Visited)
                        continue;

                    var totalDuration = currentArea.TotalDuration + neighbor.Duration;
                    if (totalDuration >= neighbor.TotalDuration)
                        continue;

                    neighbor.CameFrom = pair.Key;
                    neighbor.TotalDuration = totalDuration;

                    var priority = GetQueuePriority(neighbor, planet.Astroants);
                    if (neighbor.InQueue)
                    {
                        areaQueue.UpdatePriority(neighbor, priority);
                    }
                    else
                    {
                        areaQueue.Enqueue(neighbor, priority);
                        neighbor.InQueue = true;
                    }
                }
            }
            return null;
        }

        static int GetQueuePriority(Area currentArea, Area endArea)
        {
            return currentArea.TotalDuration + Math.Abs(currentArea.X - endArea.X) + Math.Abs(currentArea.Y - endArea.Y);
        }

        static Solution GetSolution(Planet planet)
        {
            var path = new List<Direction>(); 
            var current = planet.Astroants;
            while (current != planet.Sugar)
            {
                var opposite = current.CameFrom.GetOpposite();
                path.Add(opposite);
                current = current.Neighbors[opposite];
            }

            return new Solution
            {
                Duration = planet.Astroants.TotalDuration,
                Path = path
            };
        }
    }

    public class Solution
    {
        public int Duration { get; set; }
        public List<Direction> Path { get; set; }
    }
}
