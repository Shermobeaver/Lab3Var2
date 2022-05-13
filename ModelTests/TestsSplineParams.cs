using Xunit;
using Model;

namespace ModelTests
{
    public class TestsSplineParams
    {
        [Fact]
        public void Create()
        {
            var inputParams = new Input();
            var splineParams = new SplineParameters(inputParams);

            Assert.NotNull(splineParams);
            Assert.Equal<int>(25 * 10, splineParams.LengthUni);
            Assert.Equal<double>(10, splineParams.x1);
            Assert.Equal<double>(20, splineParams.x2);
            Assert.Equal<double>(30, splineParams.x3);
        }

        [Fact]
        public void Update()
        {
            var inputParams = new Input();
            var splineParams = new SplineParameters(inputParams);

            inputParams.LengthUni = 10 * 10;
            inputParams.x1 = 2;
            inputParams.x2 = 11.5;
            inputParams.x3 = 16.8;
            splineParams.Updater(inputParams);

            Assert.Equal<int>(10 * 10, splineParams.LengthUni);
            Assert.Equal<double>(2, splineParams.x1);
            Assert.Equal<double>(11.5, splineParams.x2);
            Assert.Equal<double>(16.8, splineParams.x3);
        }
    }
}
