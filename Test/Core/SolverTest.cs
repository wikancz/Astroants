using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Astroants.Core;
using Astroants.RestApiClient.Api;
using FluentAssertions;
using Newtonsoft.Json;
using System.IO;
using Xunit;

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
            var planet = GetPlanet(planetId).ToPlanet();
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
            var apiPlanet = GetPlanet(planetId);
            var planet = apiPlanet.ToPlanet();
            var reversed = Solver.Solve(planet);

            planet = apiPlanet.ToPlanet();
            var swap = planet.Astroants;
            planet.Astroants = planet.Sugar;
            planet.Sugar = swap;

            var regular = Solver.Solve(planet);
            
            reversed.Duration.Should().Be(regular.Duration);
        }

        static PlanetJson GetPlanet(int planetId)
        {
            var json = File.ReadAllText($"Core/planet-{planetId}.json");
            return JsonConvert.DeserializeObject<PlanetJson>(json);
        }
    }
}
