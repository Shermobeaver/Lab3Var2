using System.Windows.Media;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using ViewModel;

namespace Lab2Var2
{
    public class ThisViewActions : ActionInterface
    {
        public void AddToSeries(SeriesCollection SeriesCollection, double[] points, double[] values, string title, int mode)
        {
            ChartValues<ObservablePoint> Values = new ChartValues<ObservablePoint>();
            for (int i = 0; i < values.Length; i++)
            {
                Values.Add(new(points[i], values[i]));
            }

            if (mode == 0)
            {
                SeriesCollection.Add(new ScatterSeries
                {
                    Title = title,
                    Values = Values,
                    Fill = Brushes.Blue,
                    MinPointShapeDiameter = 5,
                    MaxPointShapeDiameter = 5
                });
            }
            // Splines
            else if (mode == 1)
            {
                SeriesCollection.Add(new LineSeries
                {
                    Title = title,
                    Values = Values,
                    Fill = Brushes.Transparent,
                    Stroke = Brushes.Red,
                    PointGeometry = null,
                    LineSmoothness = 0
                });
            }
        }

        public void ErrorMessageBox(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
