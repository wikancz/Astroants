using Astroants.Core;
using System;

namespace Astroants.RestApiClient
{
    public class LazyAreaArray : AreaArray
    {
        int _size;
        string[] _areaStrings;

        public LazyAreaArray(string[] areaStrings)
        {
            _areaStrings = areaStrings;
            _size = (int)Math.Sqrt(areaStrings.Length);
            Areas = new LazyArea[_size, _size];
        }

        protected override Area this[int x, int y]
        {
            get
            {
                var area = Areas[x, y];
                if (area != null)
                    return area;

                var areaString = _areaStrings[x + y * _size];
                return Areas[x, y] = new LazyArea(areaString, x, y);
            }
        }
    }
}
