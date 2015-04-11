using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GolfersUI
{
    public class PointsMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var points = (values[0] as ObservableCollection<Golfers.Point>).ToList();
            var solution = (values[1] as ObservableCollection<Tuple<Golfers.Point, Golfers.Point>>).ToList();

            var shapes = new List<Shape>();

            foreach (var pair in solution)
            {
                var line = new Line();
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 10;

                line.X1 = pair.Item1.X * 100 + 5000;
                line.Y1 = pair.Item1.Y * 100 + 5000;

                line.X2 = pair.Item2.X * 100 + 5000;
                line.Y2 = pair.Item2.Y * 100 + 5000;

                shapes.Add(line);
            }

            foreach(var point in points)
            {
                var ellipse = new Ellipse();
                ellipse.Fill = point.Type == Golfers.PointType.Golfer ? Brushes.Red : Brushes.Blue;
                ellipse.StrokeThickness = 2;
                ellipse.Stroke = Brushes.Black;

                ellipse.Width = 50;
                ellipse.Height = 50;

                Canvas.SetLeft(ellipse, point.X * 100 + 5000 - 25);
                Canvas.SetTop(ellipse, point.Y * 100 + 5000 - 25);
                shapes.Add(ellipse);
            }

            return shapes.ToArray();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return new List<Object>().ToArray();
        }
    }
}
