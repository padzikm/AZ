using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Golfers;

namespace GolfersCommandline
{
    public class Program
    {
        static void Main(string[] args)
        {
            var maks = 10000;
            var step = 100;
            var points = GetRandomPoints(maks, maks, maks);
            var validator = new Validator();

            while(!validator.IsDataValid(points))
                points = GetRandomPoints(maks, maks, maks);

            Console.WriteLine("Wygenerowano punkty\n");

            for (var i = 100; i <= maks; i += step)
            {
                Console.WriteLine("Rozpoczeto: " + i);

                var data = points.GetRange(0, 2*i);

                var solver = new Algorithm();

                var timer = new Stopwatch();

                timer.Start();

                var result = solver.Solve(data);

                timer.Stop();

                Console.WriteLine("Obliczono");

                var elapsed = timer.Elapsed;

                var isValid = validator.IsSolutionValid(result);

                var str = string.Format("{0}\t{1}\t{2} min {3} sek {4} ms", i, isValid, elapsed.Minutes, elapsed.Seconds, elapsed.Milliseconds);

                using (var file = File.AppendText("perf.txt"))
                    file.WriteLine(str);
            }
        }

        static List<Point> GetRandomPoints(int pairCount, double sizeX, double sizeY)
        {
            var random = new Random(DateTime.Now.Millisecond);
            var list = new List<Point>();
            var x = 0D;
            var y = 0D;
            var hole = null as Point;
            var golfer = null as Point;

            for (var i = 0; i < pairCount; ++i)
            {
                x = sizeX * random.NextDouble();
                y = sizeY * random.NextDouble();
                hole = new Point(x, y, PointType.Hole);
                list.Add(hole);

                x = sizeX * random.NextDouble();
                y = sizeY * random.NextDouble();
                golfer = new Point(x, y, PointType.Golfer);
                list.Add(golfer);
            }

            return list;
        }
    }
}
