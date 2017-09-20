using Astroants.Core;

namespace Astroants.RestApiClient
{
    public class LazyArea : Area
    {
        public LazyArea(string areaString, int x, int y)
        {
            Coords = new Coords(x, y);

            var i = 0;
            var duration = 0;
            char chr;
            while ((chr = areaString[i++]) != '-')
            {
                duration = duration * 10 + chr - 48;
            }
            Duration = duration;

            Neighbors = new Direction[areaString.Length - i];
            var n = 0;
            while (i < areaString.Length)
            {
                Neighbors[n++] = DirectionEx.FromCode(areaString[i++]);
            }
        }
    }
}
