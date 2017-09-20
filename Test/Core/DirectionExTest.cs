using Astroants.Core;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Astroants.Test.Core
{
    public class DirectionExTest
    {
        [Fact]
        public void GetCode_ReturnsCorrectCode()
        {
            Direction.Up.GetCode().Should().Be('U');
            Direction.Down.GetCode().Should().Be('D');
            Direction.Left.GetCode().Should().Be('L');
            Direction.Right.GetCode().Should().Be('R');
        }

        [Fact]
        public void GetOpposite_ReturnsCorrectDirection()
        {
            Direction.Up.GetOpposite().Should().Be(Direction.Down);
            Direction.Down.GetOpposite().Should().Be(Direction.Up);
            Direction.Left.GetOpposite().Should().Be(Direction.Right);
            Direction.Right.GetOpposite().Should().Be(Direction.Left);
        }

        [Fact]
        public void GetCodes_ReturnsCorrectString()
        {
            var directions = new List<Direction>
            {
                Direction.Down, Direction.Up, Direction.Left, Direction.Right, Direction.Right, Direction.Down
            };
            directions.GetCodes().Should().Be("DULRRD");
        }
    }
}
