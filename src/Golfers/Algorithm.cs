using System;
using System.Collections.Generic;
using System.Linq;
namespace Golfers
{
    public class Algorithm
    {
        public List<Tuple<Point, Point>> Solve(List<Point> points)
        {
            if (points == null || points.Count == 0)
                return new List<Tuple<Point, Point>>();

            var min = points[0];

            foreach (var point in points)
                if(point.Y < min.Y || (point.Y == min.Y && point.X < min.X))
                    min = point;

            points.Remove(min);

            points = PointExtensions.AngleSort(points, min);

            var sum = 0;
            var dest = points[0];

            foreach (var point in points)
            {
                if (min.Type != point.Type)
                    --sum;
                else
                    ++sum;

                if (sum == -1)
                {
                    dest = point;
                    break;
                }
            }

            var l1 = points.TakeWhile(p => p != dest).ToList();
            var l2 = points.SkipWhile(p => p != dest).ToList();
            l2.RemoveAt(0);
            var res = new Tuple<Point, Point>(min, dest);
            
            var res1 = Solve(l1);
            var res2 = Solve(l2);

            res1.AddRange(res2);
            res1.Add(res);

            points.Add(min);

            return res1;
        }
    }
}
