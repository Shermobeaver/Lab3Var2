using LiveCharts;

namespace ViewModel
{
    public class LiveChart
    {
        public SeriesCollection Series { get; set; }
        public Func<double, string> Formatter { get; set; }

        public LiveChart()
        {
            Series = new SeriesCollection();

            Formatter = value => value.ToString("F4");
        }
    }
}
