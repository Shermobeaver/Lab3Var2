namespace Model
{
    public class SplineParameters
    {
        public int LengthUni { get; set; }
        public double x1 { get; set; }
        public double x2 { get; set; }
        public double x3 { get; set; }

        public SplineParameters(Input input)
        {
            LengthUni = input.LengthUni;
            x1 = input.x1;
            x2 = input.x2;
            x3 = input.x3;
        }

        public void Updater(Input input)
        {
            LengthUni = input.LengthUni;
            x1 = input.x1;
            x2 = input.x2;
            x3 = input.x3;
        }
    }
}
