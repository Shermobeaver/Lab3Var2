using Xunit;
using Model;
using ViewModel;

namespace ViewModelTests
{
    public class TestViewModel
    {
        [Fact]
        public void Create()
        {
            var actions = new TestActions();
            var viewData = new ViewData(actions);

            Assert.NotNull(viewData);
            Assert.NotNull(viewData.Params);
            Assert.NotNull(viewData.Data);
            Assert.NotNull(viewData.ListOutput);
            Assert.NotNull(viewData.Plot);
            Assert.NotNull(viewData.ViewActions);
            Assert.NotNull(viewData.TextBlocks);
        }

        [Fact]
        public void Update()
        {
            var actions = new TestActions();
            var viewData = new ViewData(actions);

            viewData.Params.Length = 25;
            viewData.Params.Left = 1;
            viewData.Params.Right = 30;
            viewData.Params.Function = SPf.Cubic;
            viewData.Data.Measures.Updater(viewData.Params);
            Assert.Equal<int>(25, viewData.Data.Measures.Length);
            Assert.Equal<double>(1, viewData.Data.Measures.Left);
            Assert.Equal<double>(30, viewData.Data.Measures.Right);
            Assert.Equal<int>((int)SPf.Cubic, (int)viewData.Data.Measures.Function);

            viewData.Params.LengthUni = 25 * 10;
            viewData.Params.x1 = 2;
            viewData.Params.x2 = 11.5;
            viewData.Params.x3 = 16.8;
            viewData.Data.Params.Updater(viewData.Params);
            Assert.Equal<int>(25 * 10, viewData.Data.Params.LengthUni);
            Assert.Equal<double>(2, viewData.Data.Params.x1);
            Assert.Equal<double>(11.5, viewData.Data.Params.x2);
            Assert.Equal<double>(16.8, viewData.Data.Params.x3);
        }

        [Fact]
        public void Measurement()
        {
            var actions = new TestActions();
            var viewData = new ViewData(actions);

            viewData.Data.Measures.CreateGrid();
            viewData.Data.Measures.MeasureValues(viewData.ListOutput);

            Assert.Equal<int>(viewData.Data.Measures.Length, viewData.Data.Measures.Grid.Length);
            for (int i = 1; i < viewData.Data.Measures.Grid.Length; i++)
            {
                Assert.True(viewData.Data.Measures.Grid[i - 1] < viewData.Data.Measures.Grid[i]);
            }

            Assert.Equal<int>(viewData.Data.Measures.Length, viewData.Data.Measures.Values.Length);
            Assert.NotNull(viewData.ListOutput);
        }

        [Fact]
        public void Interpolate()
        {
            var actions = new TestActions();
            var viewData = new ViewData(actions);

            viewData.Data.Measures.CreateGrid();
            viewData.Data.Measures.MeasureValues(viewData.ListOutput);
            Assert.Equal<double>(0, viewData.Interpolate());
        }

        [Fact]
        public void Integrate()
        {
            var actions = new TestActions();
            var viewData = new ViewData(actions);

            viewData.Data.Measures.CreateGrid();
            viewData.Data.Measures.MeasureValues(viewData.ListOutput);
            Assert.Equal<double>(0, viewData.Interpolate());
            Assert.Equal<double>(0, viewData.Integrate());
        }

        [Fact]
        public void CanExecute()
        {
            var actions = new TestActions();
            var viewData = new ViewData(actions);

            Assert.True(viewData.ActionMeasure_CanExecute());

            viewData.ActionMeasure(0);
            Assert.True(viewData.ActionSplines_CanExecute());

            viewData.ActionSplines(0);
            Assert.False(viewData.ActionSplines_CanExecute());

            viewData = new ViewData(actions);

            viewData.Params.Length = 2; // Неправильный ввод
            viewData.Data.Measures.Updater(viewData.Params);

            Assert.False(viewData.ActionMeasure_CanExecute());

            viewData.Params.Length = 20;
            viewData.Params.x1 = 1; // Неправильный ввод
            viewData.Data.Params.Updater(viewData.Params);
            viewData.ActionMeasure(0);

            Assert.False(viewData.ActionSplines_CanExecute());
        }

        [Fact]
        public void ResaultsCheck()
        {
            var actions = new TestActions();
            var viewData = new ViewData(actions);

            viewData.Data.Measures.CreateGrid();
            viewData.Data.Measures.MeasureValues(viewData.ListOutput);
            viewData.ActionMeasure(0);
            viewData.ActionSplines(0);
            Assert.Equal<double>(viewData.Data.Derivatieves[0], viewData.TextBlocks.TextBlock_Der_1rst_l);
            Assert.Equal<double>(viewData.Data.Derivatieves[1], viewData.TextBlocks.TextBlock_Der_1rst_r);
            Assert.Equal<double>(viewData.Data.Derivatieves[2], viewData.TextBlocks.TextBlock_Der_2nd_l);
            Assert.Equal<double>(viewData.Data.Derivatieves[3], viewData.TextBlocks.TextBlock_Der_2nd_r);
            Assert.Equal<double>(viewData.Data.Splines[0], viewData.TextBlocks.TextBlock_Spl1);
            Assert.Equal<double>(viewData.Data.Splines[viewData.Data.Measures.Length - 1], viewData.TextBlocks.TextBlock_Spl2);
            Assert.Equal<double>(viewData.Data.Integrals[0], viewData.TextBlocks.TextBlock_Integ1);
            Assert.Equal<double>(viewData.Data.Integrals[1], viewData.TextBlocks.TextBlock_Integ2);
        }
    }
}