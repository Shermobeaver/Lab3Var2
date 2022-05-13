using Xunit;
using ViewModel;

namespace ViewModelTests
{
    public class TestsLiveChart
    {
        [Fact]
        public void Create()
        {
            var plot = new LiveChart();
            Assert.NotNull(plot);
            Assert.NotNull(plot.Series);
            Assert.NotNull(plot.Formatter);
        }
    }
}
