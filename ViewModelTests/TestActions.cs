using LiveCharts;
using ViewModel;

namespace ViewModelTests
{
    internal class TestActions : ActionInterface
    {
        public void AddToSeries(SeriesCollection SeriesCollection, double[] points, double[] values, string title, int mode)
        {
        }

        public void ErrorMessageBox(string error)
        {
            throw new System.Exception(error);
        }
    }
}
