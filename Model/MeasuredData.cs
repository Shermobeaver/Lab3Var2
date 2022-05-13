using System.Collections.ObjectModel;

namespace Model
{
    public class MeasuredData
    {
        public int Length { get; set; }
        public double Left { get; set; }
        public double Right { get; set; }
        public SPf Function { get; set; }
        public double[] Grid { get; set; }
        public double[] Values { get; set; }

        public MeasuredData(Input input)
        {
            Length = input.Length;
            Left = input.Left;
            Right = input.Right;
            Function = input.Function;
        }

        public void Updater(Input input)
        {
            Length = input.Length;
            Left = input.Left;
            Right = input.Right;
            Function = input.Function;
        }

        public void CreateGrid()
        {
            Grid = new double[Length];
            var rand = new Random();

            Grid[0] = Left;
            Grid[Length - 1] = Right;
            for (int i = 1; i < Length - 1; i++)
            {
                double random = Left; 
                while(random <= Left)
                {
                    random = Right * rand.NextDouble();
                }
                Grid[i] = random;
            }

            Array.Sort(Grid);
        }

        public void MeasureValues(ObservableCollection<string>  ListOutput)
        {
            Values = new double[Length];

            if (Function == SPf.Linear)
            {
                for (int i = 0; i < Length; i++)
                {
                    Values[i] = Grid[i];
                }
            }
            else if (Function == SPf.Cubic)
            {
                for (int i = 0; i < Length; i++)
                {
                    Values[i] = Math.Pow(Grid[i], 3);
                }
            }
            else if (Function == SPf.Random)
            {
                var rand = new Random();
                for (int i = 0; i < Length; i++)
                {
                    Values[i] = 20 * rand.NextDouble();
                }
            }

            ListOutput.Clear();
            for (int i = 0; i < Length; i++)
            {
                ListOutput.Add($"Точка сетки: {Grid[i]}\nЗначение: {Values[i]}\n");
            }
        }
    }
}
