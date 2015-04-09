namespace Golfers
{
    public class Point
    {
        public double X { get; private set; }

        public double Y { get; private set; }

        public PointType Type { get; private set; }

        public Point(double x, double y, PointType type)
        {
            X = x;
            Y = y;
            Type = type;
        }
    }
}
