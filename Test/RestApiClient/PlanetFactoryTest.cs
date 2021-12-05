using Astroants.Core;
using Astroants.RestApiClient.Api;
using FluentAssertions;
using Xunit;

namespace Astroants.Test.RestApiClient
{
    public class PlanetFactoryTest
    {
        [Fact]
        public void CreatesPlanet()
        {
            var planetJson = new PlanetJson
            {
                Id = 2727,
                StartedTimestamp = 1503929807498,
                Map = new Map
                {
                    Areas = new string[] { "5-R", "1-RDL", "10-DL", "2-RD", "1-UL", "1-UD", "2-RU", "1-RL", "2-UL" }
                },
                Astroants = new Coords { X = 1, Y = 0 },
                Sugar = new Coords { X = 2, Y = 1 }
            };

            var planet = planetJson.ToPlanet();

            planet.Id.Should().Be(2727);
            planet.Areas.Length.Should().Be(9);

            var coords = new Coords(0, 0);
            var area = planet.Areas[coords];
            area.Duration.Should().Be(5);
            area.Neighbors.Length.Should().Be(1);
            area.Neighbors[0].Should().Be(Direction.Right);

            coords.X = 1;
            area = planet.Areas[coords];
            planet.Astroants.Should().Be(coords);
            area.Duration.Should().Be(1);
            area.Neighbors.Length.Should().Be(3);
            area.Neighbors[0].Should().Be(Direction.Right);
            area.Neighbors[1].Should().Be(Direction.Down);
            area.Neighbors[2].Should().Be(Direction.Left);

            coords.X = 2;
            area = planet.Areas[coords];
            area.Duration.Should().Be(10);
            area.Neighbors.Length.Should().Be(2);
            area.Neighbors[0].Should().Be(Direction.Down);
            area.Neighbors[1].Should().Be(Direction.Left);

            coords.X = 0;
            coords.Y = 1;
            area = planet.Areas[coords];
            area.Duration.Should().Be(2);
            area.Neighbors.Length.Should().Be(2);
            area.Neighbors[0].Should().Be(Direction.Right);
            area.Neighbors[1].Should().Be(Direction.Down);

            coords.X = 1;
            area = planet.Areas[coords];
            area.Duration.Should().Be(1);
            area.Neighbors.Length.Should().Be(2);
            area.Neighbors[0].Should().Be(Direction.Up);
            area.Neighbors[1].Should().Be(Direction.Left);

            coords.X = 2;
            area = planet.Areas[coords];
            planet.Sugar.Should().Be(coords);
            area.Duration.Should().Be(1);
            area.Neighbors.Length.Should().Be(2);
            area.Neighbors[0].Should().Be(Direction.Up);
            area.Neighbors[1].Should().Be(Direction.Down);

            coords.X = 0;
            coords.Y = 2;
            area = planet.Areas[coords];
            area.Duration.Should().Be(2);
            area.Neighbors.Length.Should().Be(2);
            area.Neighbors[0].Should().Be(Direction.Right);
            area.Neighbors[1].Should().Be(Direction.Up);

            coords.X = 1;
            coords.Y = 2;
            area = planet.Areas[coords];
            area.Duration.Should().Be(1);
            area.Neighbors.Length.Should().Be(2);
            area.Neighbors[0].Should().Be(Direction.Right);
            area.Neighbors[1].Should().Be(Direction.Left);

            coords.X = 2;
            coords.Y = 2;
            area = planet.Areas[coords];
            area.Duration.Should().Be(2);
            area.Neighbors.Length.Should().Be(2);
            area.Neighbors[0].Should().Be(Direction.Up);
            area.Neighbors[1].Should().Be(Direction.Left);

            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    area = planet.Areas[new Coords(x, y)];
                    area.Coords.X.Should().Be(x);
                    area.Coords.Y.Should().Be(y);
                    area.DurationFromStart.Should().Be(int.MaxValue);
                }
            }
        }
    }
}
