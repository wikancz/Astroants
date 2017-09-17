using Astroants.Core;
using System;

namespace Astroants.RestApiClient
{
    public static class PlanetFactory
    {
        public static Planet Create(Api.Planet planetJson)
        {
            var map = planetJson.Map;
            var sugar = planetJson.Sugar;
            var astroants = planetJson.Astroants;

            var planet = new Planet { Id = planetJson.Id };

            var size = (int)Math.Sqrt(map.Areas.Length);
            InitAreas(planet, size);
            FillAreas(planet.Areas, map.Areas);

            planet.Sugar = planet.Areas[sugar.X, sugar.Y];
            planet.Sugar.Duration = 0;

            planet.Astroants = planet.Areas[astroants.X, astroants.Y];
            planet.Astroants.Duration = 0;

            return planet;
        }

        static void InitAreas(Planet planet, int size)
        {
            planet.Areas = new Area[size, size];
            for (var x = 0; x < size; x++)
            {
                for (var y = 0; y < size; y++)
                {
                    planet.Areas[x, y] = new Area { X = x, Y = y };
                }
            }
        }

        static void FillAreas(Area[,] areas, string[] areaStrings)
        {
            var x = 0;
            var y = 0;
            var size = areas.GetLength(0);
            for (var i = 0; i < areaStrings.Length; i++)
            {
                if (x == size)
                {
                    x = 0;
                    y++;
                }

                var area = areas[x, y];

                var areaString = areaStrings[i];
                var dashIndex = areaString.IndexOf('-');
                area.Duration = int.Parse(areaString.Substring(0, dashIndex));

                for (var d = dashIndex + 1; d < areaString.Length; d++)
                {
                    switch (areaString[d])
                    {
                        case 'U':
                            area.Neighbors[Direction.Up] = areas[x, y - 1];
                            break;
                        case 'D':
                            area.Neighbors[Direction.Down] = areas[x, y + 1];
                            break;
                        case 'L':
                            area.Neighbors[Direction.Left] = areas[x - 1, y];
                            break;
                        case 'R':
                            area.Neighbors[Direction.Right] = areas[x + 1, y];
                            break;
                    }
                }

                x++;
            }
        }
    }
}
