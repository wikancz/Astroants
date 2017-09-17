using Astroants.Core;
using Astroants.RestApiClient;
using Astroants.RestApiClient.Api;
using FluentAssertions;
using Xunit;
using ApiPlanet = Astroants.RestApiClient.Api.Planet;

namespace Astroants.Test.RestApiClient
{
    public class PlanetFactoryTest
    {
        [Fact]
        public void CreatesPlanet()
        {
            var planetJson = new ApiPlanet
            {
                Id = 2727,
                StartedTimestamp = 1503929807498,
                Map = new Map
                {
                    Areas = new string[] { "5-R", "1-RDL", "10-DL", "2-RD", "1-UL", "1-UD", "2-RU", "1-RL", "2-UL" }
                },
                Astroants = new Point { X = 1, Y = 0 },
                Sugar = new Point { X = 2, Y = 1 }
            };

            var planet = PlanetFactory.Create(planetJson);

            planet.Id.Should().Be(2727);
            planet.Areas.Length.Should().Be(9);

            var area = planet.Areas[0, 0];
            area.Duration.Should().Be(5);
            area.Neighbors.Count.Should().Be(1);
            area.Neighbors[Direction.Right].Should().Be(planet.Areas[1, 0]);

            area = planet.Areas[1, 0];
            planet.Astroants.Should().Be(area);
            area.Duration.Should().Be(0);
            area.Neighbors.Count.Should().Be(3);
            area.Neighbors[Direction.Right].Should().Be(planet.Areas[2, 0]);
            area.Neighbors[Direction.Down].Should().Be(planet.Areas[1, 1]);
            area.Neighbors[Direction.Left].Should().Be(planet.Areas[0, 0]);

            area = planet.Areas[2, 0];
            area.Duration.Should().Be(10);
            area.Neighbors.Count.Should().Be(2);
            area.Neighbors[Direction.Down].Should().Be(planet.Areas[2, 1]);
            area.Neighbors[Direction.Left].Should().Be(planet.Areas[1, 0]);

            area = planet.Areas[0, 1];
            area.Duration.Should().Be(2);
            area.Neighbors.Count.Should().Be(2);
            area.Neighbors[Direction.Right].Should().Be(planet.Areas[1, 1]);
            area.Neighbors[Direction.Down].Should().Be(planet.Areas[0, 2]);

            area = planet.Areas[1, 1];
            area.Duration.Should().Be(1);
            area.Neighbors.Count.Should().Be(2);
            area.Neighbors[Direction.Up].Should().Be(planet.Areas[1, 0]);
            area.Neighbors[Direction.Left].Should().Be(planet.Areas[0, 1]);

            area = planet.Areas[2, 1];
            planet.Sugar.Should().Be(area);
            area.Duration.Should().Be(0);
            area.Neighbors.Count.Should().Be(2);
            area.Neighbors[Direction.Up].Should().Be(planet.Areas[2, 0]);
            area.Neighbors[Direction.Down].Should().Be(planet.Areas[2, 2]);

            area = planet.Areas[0, 2];
            area.Duration.Should().Be(2);
            area.Neighbors.Count.Should().Be(2);
            area.Neighbors[Direction.Right].Should().Be(planet.Areas[1, 2]);
            area.Neighbors[Direction.Up].Should().Be(planet.Areas[0, 1]);

            area = planet.Areas[1, 2];
            area.Duration.Should().Be(1);
            area.Neighbors.Count.Should().Be(2);
            area.Neighbors[Direction.Right].Should().Be(planet.Areas[2, 2]);
            area.Neighbors[Direction.Left].Should().Be(planet.Areas[0, 2]);

            area = planet.Areas[2, 2];
            area.Duration.Should().Be(2);
            area.Neighbors.Count.Should().Be(2);
            area.Neighbors[Direction.Up].Should().Be(planet.Areas[2, 1]);
            area.Neighbors[Direction.Left].Should().Be(planet.Areas[1, 2]);

            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    area = planet.Areas[x, y];
                    area.X.Should().Be(x);
                    area.Y.Should().Be(y);
                    area.TotalDuration.Should().Be(int.MaxValue);
                }
            }
        }
    }
}
