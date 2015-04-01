using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace AZ
{
    class DataIO
    {
        public static List<Point> Read(string path)
        {
            var list1 = new List<Point>();
            var list2 = new List<Point>();

            using (var reader = File.OpenText(path))
            {
                var line = reader.ReadLine();

                var counter = int.Parse(line);

                for (var i = 0; i < counter; ++i)
                {
                    line = reader.ReadLine();
                    var numbers = line.Split(' ');
                    var first = double.Parse(numbers[0], CultureInfo.InvariantCulture);
                    var second = double.Parse(numbers[1], CultureInfo.InvariantCulture);

                    list1.Add(new Point(first, second, PointType.Golfer));
                }

                reader.ReadLine();

                for (var i = 0; i < counter; ++i)
                {
                    line = reader.ReadLine();
                    var numbers = line.Split(' ');
                    var first = double.Parse(numbers[0], CultureInfo.InvariantCulture);
                    var second = double.Parse(numbers[1], CultureInfo.InvariantCulture);

                    list2.Add(new Point(first, second, PointType.Hole));
                }
            }

            list1.AddRange(list2);

            return list1;
        }

        public static void Write(string path, List<Tuple<Point, Point>> data)
        {
            using (var writer = File.CreateText(path))
            {
                var first = true;

                foreach (var pair in data)
                {
                    if(!first)
                        writer.WriteLine();

                    if (pair.Item1.Type == PointType.Golfer)
                    {
                        writer.WriteLine("{0} {1}", pair.Item1.X, pair.Item1.Y);
                        writer.WriteLine("{0} {1}", pair.Item2.X, pair.Item2.Y);
                    }
                    else
                    {
                        writer.WriteLine("{0} {1}", pair.Item2.X, pair.Item2.Y);
                        writer.WriteLine("{0} {1}", pair.Item1.X, pair.Item1.Y);
                    }

                    first = false;
                }
            }
        }
    }
}
