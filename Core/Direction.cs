using System;
using System.Collections.Generic;

namespace Astroants.Core
{
    public enum Direction
    {
        Up = 1,
        Left,
        Right,
        Down
    }

    public static class DirectionEx
    {
        public static char GetCode(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return 'U';
                case Direction.Left:
                    return 'L';
                case Direction.Right:
                    return 'R';
                case Direction.Down:
                    return 'D';
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction));
            }
        }

        public static Direction GetOpposite(this Direction direction)
        {
            return 5 - direction;
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

        public static Direction FromCode(char code)
        {
            switch (code)
            {
                case 'U':
                    return Direction.Up;
                case 'L':
                    return Direction.Left;
                case 'R':
                    return Direction.Right;
                case 'D':
                    return Direction.Down;
                default:
                    throw new ArgumentOutOfRangeException(nameof(code));
            }
        }
    }
}
