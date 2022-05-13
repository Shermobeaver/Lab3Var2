using System.Collections.ObjectModel;
using Xunit;
using Model;

namespace ModelTests
{
    public class TestsSplinesData
    {
        [Fact]
        public void Create()
        {
            var inputParams = new Input();
            var measured = new MeasuredData(inputParams);
            var splineParams = new SplineParameters(inputParams);
            var data = new SplinesData(measured, splineParams);

            Assert.NotNull(data);
            Assert.NotNull(data.Measures);
            Assert.NotNull(data.Params);
            Assert.Equal<int>(2, data.Integrals.Length);
            Assert.Equal<int>(4, data.Derivatieves.Length);
        }

        [Fact]
        public void Interpolate()
        {
            ObservableCollection<string> ListContents = new();
            var inputParams = new Input();
            var measured = new MeasuredData(inputParams);
            var splineParams = new SplineParameters(inputParams);
            var data = new SplinesData(measured, splineParams);
            data.Measures.CreateGrid();
            data.Measures.MeasureValues(ListContents);

            Assert.Equal<double>(0, data.Interpolate());
        }

        [Fact]
        public void Integrate()
        {
            ObservableCollection<string> ListContents = new();
            var inputParams = new Input();
            var measured = new MeasuredData(inputParams);
            var splineParams = new SplineParameters(inputParams);
            var data = new SplinesData(measured, splineParams);
            data.Measures.CreateGrid();
            data.Measures.MeasureValues(ListContents);

            Assert.Equal<double>(0, data.Interpolate());
            Assert.Equal<double>(0, data.Integrate());
        }
    }
}
