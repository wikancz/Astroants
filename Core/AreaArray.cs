using System;

namespace Astroants.Core
{
    public class AreaArray
    {
        protected Area[,] Areas; 

        public Area this[Coords coords] => this[coords.X, coords.Y];

        public int Length => Areas.Length;

        public Area GetNeighbor(Coords area, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return this[area.X, area.Y - 1];
                case Direction.Left:
                    return this[area.X - 1, area.Y];
                case Direction.Right:
                    return this[area.X + 1, area.Y];
                case Direction.Down:
                    return this[area.X, area.Y + 1];
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction));
            }
        }

        protected virtual Area this[int x, int y] => Areas[x, y];
    }
}
