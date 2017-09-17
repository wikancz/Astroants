using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Astroants.Core;
using Astroants.RestApiClient;
using FluentAssertions;
using Newtonsoft.Json;
using System.IO;
using Xunit;
using ApiPlanet = Astroants.RestApiClient.Api.Planet;

namespace Astroants.Test.Core
{
    public class SolverTest
    {
        [Theory]
        [InlineData(1096)]
        [InlineData(1097)]
        [InlineData(1098)]
        [InlineData(1099)]
        [InlineData(1100)]
        [InlineData(1134)]
        [InlineData(1207)]
        [InlineData(1209)]
        [UseReporter(typeof(DiffReporter))]
        public void FindsPath(int planetId)
        {
            var planet = GetPlanet(planetId);
            var solution = Solver.Solve(planet);
            var expected = $"{{\"path\":\"{solution.Path.GetCodes()}\"}}";

            NamerFactory.AdditionalInformation = planetId.ToString();
            Approvals.Verify(expected);
        }

        [Theory]
        [InlineData(1096)]
        [InlineData(1097)]
        [InlineData(1098)]
        [InlineData(1099)]
        [InlineData(1100)]
        [InlineData(1134)]
        [InlineData(1207)]
        [InlineData(1209)]
        public void AstroantsAndSugarCanBeSwapped(int planetId)
        {
            var planet = GetPlanet(planetId);
            var reversed = Solver.Solve(planet);

            planet = GetPlanet(planetId);
            var swap = planet.Astroants;
            planet.Astroants = planet.Sugar;
            planet.Sugar = swap;

            var regular = Solver.Solve(planet);
            
            reversed.Duration.ShouldBeEquivalentTo(regular.Duration);
        }

        static Planet GetPlanet(int planetId)
        {
            var json = File.ReadAllText($"Core/planet-{planetId}.json");
            var planet = JsonConvert.DeserializeObject<ApiPlanet>(json);
            return PlanetFactory.Create(planet);
        }
    }
}
