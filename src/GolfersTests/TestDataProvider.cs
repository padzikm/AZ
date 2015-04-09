using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Golfers;

namespace GolfersTests
{
    public class TestDataProvider
    {
        public List<Point> GetSmallValidData()
        {
            return new List<Point>() { new Point(0, 0, PointType.Golfer), new Point(1, 0, PointType.Golfer), new Point(0, 1, PointType.Golfer), new Point(10, 10, PointType.Hole), new Point(10, 5, PointType.Hole), new Point(5, 10, PointType.Hole) };
        }

        public List<Point> GetSmallInvalidDataWithMoreHoles()
        {
            return new List<Point>() { new Point(1, 0, PointType.Golfer), new Point(0, 1, PointType.Golfer), new Point(10, 10, PointType.Hole), new Point(10, 5, PointType.Hole), new Point(5, 10, PointType.Hole) };
        }

        public List<Point> GetRandomValidSquareData(int count)
        {
            var points = new List<Point>();

            for (double i = 1.0; i <= count; i += 1.0 )
            {
                points.Add(new Point(i, i * i, PointType.Golfer));
                points.Add(new Point(-i, i * i, PointType.Hole));
            }

            return points;
        }

        public List<Tuple<Point, Point>> GetValidSolution()
        {
            return new List<Tuple<Point, Point>>(){ new Tuple<Point, Point>(new Point(0, 0, PointType.Golfer), new Point(10, 10, PointType.Hole)),
                                                   new Tuple<Point, Point>(new Point(1, 0, PointType.Golfer), new Point(10, 5, PointType.Hole)),
                                                   new Tuple<Point, Point>(new Point(0, 1, PointType.Golfer), new Point(5, 10, PointType.Hole))
            };
        }

        public List<Tuple<Point, Point>> GetInvalidSolution()
        {
            return new List<Tuple<Point, Point>>(){ new Tuple<Point, Point>(new Point(0, 0, PointType.Golfer), new Point(10, 10, PointType.Hole)),
                                                   new Tuple<Point, Point>(new Point(1, 0, PointType.Golfer), new Point(5, 10, PointType.Hole)),
                                                   new Tuple<Point, Point>(new Point(0, 1, PointType.Golfer), new Point(10, 5, PointType.Hole))
            };
        }

    }
}
