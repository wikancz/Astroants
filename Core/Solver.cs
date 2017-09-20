using System.Collections.Generic;
using Priority_Queue;

namespace Astroants.Core
{
    public static class Solver
    {
        public static Solution Solve(Planet planet)
        {
            var areas = planet.Areas;

            var currentArea = areas[planet.Sugar];
            currentArea.Duration = 0;
            currentArea.DurationFromStart = 0;

            var endArea = planet.Areas[planet.Astroants];
            endArea.Duration = 0;

            var areaQueue = new FastPriorityQueue<Area>(areas.Length);

            while (true)
            {
                if (currentArea == endArea)
                    return GetSolution(planet);

                currentArea.Visited = true;

                for (var i = 0; i < currentArea.Neighbors.Length; i++)
                {
                    var direction = currentArea.Neighbors[i];
                    var neighbor = areas.GetNeighbor(currentArea.Coords, direction);
                    if (neighbor.Visited)
                        continue;

                    var durationFromStart = currentArea.DurationFromStart + neighbor.Duration;
                    if (durationFromStart >= neighbor.DurationFromStart)
                        continue;

                    neighbor.CameFrom = direction;
                    neighbor.DurationFromStart = durationFromStart;

                    if (areaQueue.Contains(neighbor))
                    {
                        areaQueue.UpdatePriority(neighbor, neighbor.TotalEstimatedDuration);
                    }
                    else
                    {
                        neighbor.SetEstimatedDurationTo(endArea.Coords);
                        areaQueue.Enqueue(neighbor, neighbor.TotalEstimatedDuration);                        
                    }
                }

                if (areaQueue.Count == 0)
                    return null;

                currentArea = areaQueue.Dequeue();
            }
        }

        static Solution GetSolution(Planet planet)
        {
            var areas = planet.Areas;
            var current = areas[planet.Astroants];
            var endArea = areas[planet.Sugar];

            var solution = new Solution { Duration = current.DurationFromStart, Path = new List<Direction>() };

            while (current != endArea)
            {
                var opposite = current.CameFrom.GetOpposite();
                solution.Path.Add(opposite);
                current = areas.GetNeighbor(current.Coords, opposite);
            }

            return solution;
        }
    }
}
