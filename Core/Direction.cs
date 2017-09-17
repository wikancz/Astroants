using System;
using System.Collections.Generic;

namespace Astroants.Core
{
    public enum Direction
    {
        Up = 1,
        Down,
        Left,
        Right
    }

    public static class DirectionEx
    {
        public static char GetCode(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return 'U';
                case Direction.Down:
                    return 'D';
                case Direction.Left:
                    return 'L';
                case Direction.Right:
                    return 'R';
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction));
            }
        }

        public static Direction GetOpposite(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction));
            }
        }

        public static string GetCodes(this List<Direction> path)
        {
            var chars = new char[path.Count];
            for (var i = 0; i < path.Count; i++)
            {
                chars[i] = path[i].GetCode();
            }
            return new string(chars);
        }
    }
}
