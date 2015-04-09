using System;
using System.Collections.Generic;

namespace Golfers
{
    public static class PointExtensions
    {
        /// <summary>
        /// Iloczyn wektorowy
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double CrossProduct(this Point p1, Point p2)
        {
            return p1.X*p2.Y - p2.X*p1.Y;
        }

        /// <summary>
        /// Iloczyn wektorowy wzgledem innego punktu
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="relative"></param>
        /// <returns></returns>
        public static double CrossProduct(this Point p1, Point p2, Point relative)
        {
            return (p1.X - relative.X) * (p2.Y - relative.Y) - (p2.X - relative.X) * (p1.Y - relative.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static List<Point> AngleSort(List<Point> points, Point p)
        {
            points.Sort((p1, p2) => p1.CrossProduct(p2, p) > 0 ? -1 : 1);

            return points;
        }

        /// <summary>
        /// Metoda zwraca informację czy punkty są współliniowe
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static bool ArePointsCollinear(Point p1, Point p2, Point p3)
        {
            return (p2.Y - p1.Y)*(p3.X - p2.X) == (p3.Y - p2.Y)*(p2.X - p1.X);
        }
    }
}
