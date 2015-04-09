using System;
using System.Collections.Generic;

namespace Golfers
{
    public class Validator
    {
        public bool IsSolutionValid(List<Tuple<Point, Point>> mapping)
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

        public bool IsDataValid(List<Point> points)
        {
            int golfersCount = 0;
            int holesCount = 0;

            for (int i = 0; i < points.Count; ++i)
            {
                if (points[i].Type == PointType.Golfer)
                    ++golfersCount;
                else
                    ++holesCount;

                for (int j = i + 1; j < points.Count; ++j)
                    for (int k = j + 1; k < points.Count; ++k)
                        if (PointExtensions.ArePointsCollinear(points[i], points[j], points[k]))
                            return false;
            }

            return golfersCount == holesCount;
        }
    }
}
