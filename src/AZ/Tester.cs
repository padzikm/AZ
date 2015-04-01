using System;
using System.Collections.Generic;

namespace AZ
{
    class Tester
    {
        public static bool Test(List<Tuple<Point, Point>> mapping)
        {
            for (var i = 0; i < mapping.Count - 1; ++i)
                for (var j = i + 1; j < mapping.Count; ++j)
                {
                    var d1 = mapping[i].Item1.CrossProduct(mapping[j].Item1, mapping[i].Item2);
                    var d2 = mapping[i].Item1.CrossProduct(mapping[j].Item2, mapping[i].Item2);
                    var d3 = mapping[j].Item1.CrossProduct(mapping[i].Item1, mapping[j].Item2);
                    var d4 = mapping[j].Item1.CrossProduct(mapping[i].Item2, mapping[j].Item2);

                    if (((d1 > 0 && d2 < 0) || (d1 < 0 && d2 > 0)) && ((d3 > 0 && d4 < 0) || (d3 < 0 && d4 > 0)))
                        return false;
                }

            return true;
        }
    }
}
