using Xunit;
using Model;
using System.Collections.ObjectModel;
using System;

namespace ModelTests
{
    public class TestsMeasured
    {
        [Fact]
        public void Create()
        {
            var inputParams = new Input();
            var measured = new MeasuredData(inputParams);

            Assert.NotNull(measured);
            Assert.Equal<int>(inputParams.Length, measured.Length);
            Assert.Equal<double>(inputParams.Left, measured.Left);
            Assert.Equal<double>(inputParams.Right, measured.Right);
            Assert.Equal<int>((int)inputParams.Function, (int)measured.Function);
        }

        [Fact]
        public void Update()
        {
            var inputParams = new Input();
            var measured = new MeasuredData(inputParams);

            inputParams.Length = 25;
            inputParams.Left = 1;
            inputParams.Right = 30;
            inputParams.Function = SPf.Linear;
            measured.Updater(inputParams);

            Assert.Equal<int>(25, measured.Length);
            Assert.Equal<double>(1, measured.Left);
            Assert.Equal<double>(30, measured.Right);
            Assert.Equal<int>((int)SPf.Linear, (int)measured.Function);
        }

        [Fact]
        public void Grid()
        {
            var inputParams = new Input();
            var measured = new MeasuredData(inputParams);
            measured.CreateGrid();

            Assert.Equal<int>(measured.Length, measured.Grid.Length);
            for(int i = 1; i < measured.Grid.Length; i++)
            {
                Assert.True(measured.Grid[i - 1] < measured.Grid[i]);
            }
        }

        [Fact]
        public void Linear()
        {
            var inputParams = new Input();
            inputParams.Function = SPf.Linear;
            var measured = new MeasuredData(inputParams);
            measured.CreateGrid();
            ObservableCollection<string> resault = new();
            measured.MeasureValues(resault);

            Assert.Equal<int>(measured.Length, measured.Values.Length);
            for (int i = 0; i < measured.Grid.Length; i++)
            {
                Assert.Equal<double>(measured.Grid[i], measured.Values[i]);
            }
        }

        [Fact]
        public void Cubic()
        {
            var inputParams = new Input();
            inputParams.Function = SPf.Cubic;
            var measured = new MeasuredData(inputParams);
            measured.CreateGrid();
            ObservableCollection<string> resault = new();
            measured.MeasureValues(resault);

            Assert.Equal<int>(measured.Length, measured.Values.Length);
            for (int i = 0; i < measured.Grid.Length; i++)
            {
                Assert.Equal<double>(Math.Pow(measured.Grid[i], 3), measured.Values[i]);
            }
        }

        [Fact]
        public void Random()
        {
            var inputParams = new Input();
            inputParams.Function = SPf.Random;
            var measured = new MeasuredData(inputParams);
            measured.CreateGrid();
            ObservableCollection<string> resault = new();
            measured.MeasureValues(resault);

            Assert.Equal<int>(measured.Length, measured.Values.Length);
        }
    }
}
