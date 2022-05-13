using System.Collections.ObjectModel;
using Model;

namespace ViewModel
{
    public class ViewData
    {
        public Input Params { get; set; }
        public SplinesData Data { get; set; }
        public LiveChart Plot { get; set; }
        public ObservableCollection<string> ListOutput { get; set; }
        public TextBlocks TextBlocks { get; set; }
        public ActionInterface ViewActions { get; set; }

        public bool IsMeasured { get; set; }
        public bool IsSplined { get; set; }

        public ViewData(ActionInterface inViewActions)
        {
            ViewActions = inViewActions;

            Params = new();
            Data = new(new(Params), new(Params));
            Plot = new();
            ListOutput = new();
            TextBlocks = new();
        }

        public bool ActionMeasure_CanExecute()
        {
            return !Params.Error1;
        }
        public void ActionMeasure(object obj)
        {
            try
            {
                TextBlocks.SetDefaults();

                Data.Measures.Updater(Params);
                Data.Measures.CreateGrid();
                Data.Measures.MeasureValues(ListOutput);

                IsMeasured = true;
                IsSplined = false;

                Plot.Series.Clear();
                ViewActions.AddToSeries(Plot.Series, Data.Measures.Grid, Data.Measures.Values, "Функция", 0);
            }
            catch (Exception error)
            {
                ViewActions.ErrorMessageBox($"Unexpected error: {error.Message}.");
            }
        }

        public bool ActionSplines_CanExecute()
        {
            return (!Params.Error2) && IsMeasured && (!IsSplined);
        }
        public void ActionSplines(object obj)
        {
            try
            {
                Data.Params.Updater(Params);

                IsSplined = true;

                double error_vlue = Interpolate();
                if (error_vlue == 0)
                {
                    TextBlocks.TextBlock_Der_1rst_l = Data.Derivatieves[0];
                    TextBlocks.TextBlock_Der_1rst_r = Data.Derivatieves[1];
                    TextBlocks.TextBlock_Der_2nd_l = Data.Derivatieves[2];
                    TextBlocks.TextBlock_Der_2nd_r = Data.Derivatieves[3];
                    TextBlocks.TextBlock_Spl1 = Data.Splines[0];
                    TextBlocks.TextBlock_Spl2 = Data.Splines[Data.Measures.Length - 1];

                    double[] gridUniform = new double[Data.Params.LengthUni];
                    double step = (Data.Measures.Right - Data.Measures.Left) / (Data.Params.LengthUni - 1);
                    for (int i = 0; i < Data.Params.LengthUni; i++)
                    {
                        gridUniform[i] = Data.Measures.Left + (i * step);
                    }
                    ViewActions.AddToSeries(Plot.Series, gridUniform, Data.Splines, "Сплайн", 1);

                    error_vlue = Integrate();
                    if (error_vlue == 0)
                    {
                        TextBlocks.TextBlock_Integ1 = Data.Integrals[0];
                        TextBlocks.TextBlock_Integ2 = Data.Integrals[1];
                    }
                    else
                    {
                        ViewActions.ErrorMessageBox($"Error in Integration: {error_vlue}.");
                    }
                }
                else
                {
                    ViewActions.ErrorMessageBox($"Error in Interpolation: {error_vlue}.");
                }
            }
            catch (Exception error)
            {
                ViewActions.ErrorMessageBox($"Unexpected error: {error.Message}.");
            }
        }

        public double Interpolate()
        {
            return Data.Interpolate();
        }

        public double Integrate()
        {
            return Data.Integrate();
        }
    }
}
