using Xunit;
using Model;

namespace ModelTests
{
    public class TestsInput
    {
        [Fact]
        public void DefaultValues()
        {
            var inputParams = new Input();

            Assert.NotNull(inputParams);
            Assert.Equal<int>(25, inputParams.Length);
            Assert.Equal<int>(25 * 10, inputParams.LengthUni);
            Assert.Equal<double>(10, inputParams.Left);
            Assert.Equal<double>(30, inputParams.Right);
            Assert.Equal<int>((int)SPf.Linear, (int)inputParams.Function);
            Assert.Equal<double>(10, inputParams.x1);
            Assert.Equal<double>(20, inputParams.x2);
            Assert.Equal<double>(30, inputParams.x3);
        }

        [Fact]
        public void BadInput()
        {
            var inputParams = new Input();

            inputParams.Length = 1;
            Assert.True(inputParams.Error1);

            inputParams.Length = 4;
            Assert.False(inputParams.Error1);

            inputParams.Left = 89;
            Assert.True(inputParams.Error1);

            inputParams.Right = -90;
            Assert.True(inputParams.Error1);

            inputParams.LengthUni = 0;
            Assert.True(inputParams.Error2);

            inputParams.LengthUni = 10;
            Assert.False(inputParams.Error2);

            inputParams = new Input();

            inputParams.x1 = -1;
            Assert.True(inputParams.Error2);

            inputParams.x2 = -2;
            Assert.True(inputParams.Error2);

            inputParams.x3 = -3;
            Assert.True(inputParams.Error2);

            inputParams = new Input();

            inputParams.x1 = 10;
            Assert.False(inputParams.Error2);

            inputParams.x2 = 11;
            Assert.False(inputParams.Error2);

            inputParams.x3 = 12;
            Assert.False(inputParams.Error2);
        }
    }
}