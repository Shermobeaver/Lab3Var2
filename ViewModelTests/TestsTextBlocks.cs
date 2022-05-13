using Xunit;
using ViewModel;

namespace ViewModelTests
{
    public class TestsTextBlocks
    {
        [Fact]
        public void SetDefault()
        {
            var TextBlocks = new TextBlocks();

            TextBlocks.TextBlock_Der_1rst_l = 20;
            TextBlocks.TextBlock_Der_1rst_r = 20;
            TextBlocks.TextBlock_Der_2nd_l = 20;
            TextBlocks.TextBlock_Der_2nd_r = 20;
            TextBlocks.TextBlock_Integ1 = 20;
            TextBlocks.TextBlock_Integ2 = 20;
            TextBlocks.TextBlock_Spl1 = 20;
            TextBlocks.TextBlock_Spl2 = 20;

            TextBlocks.SetDefaults();

            Assert.Equal<double>(0, TextBlocks.TextBlock_Der_1rst_l);
            Assert.Equal<double>(0, TextBlocks.TextBlock_Der_1rst_r);
            Assert.Equal<double>(0, TextBlocks.TextBlock_Der_2nd_l);
            Assert.Equal<double>(0, TextBlocks.TextBlock_Der_2nd_r);
            Assert.Equal<double>(0, TextBlocks.TextBlock_Integ1);
            Assert.Equal<double>(0, TextBlocks.TextBlock_Integ2);
            Assert.Equal<double>(0, TextBlocks.TextBlock_Spl1);
            Assert.Equal<double>(0, TextBlocks.TextBlock_Spl2);
        }
    }
}
