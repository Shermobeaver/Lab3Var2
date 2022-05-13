using LiveCharts;

namespace ViewModel
{
    public interface ActionInterface
    {
        public void AddToSeries(SeriesCollection SeriesCollection, double[] points, double[] values, string title, int mode);

        public void ErrorMessageBox(string error);
    }
}
