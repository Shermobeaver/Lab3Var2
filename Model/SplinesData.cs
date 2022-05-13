using System.Linq;
using System.Runtime.InteropServices;

namespace Model
{
    public class SplinesData
    {
        public SplineParameters Params { get; set; }
        public MeasuredData Measures { get; set; }
        public double[] Splines { get; set; }
        public double[] Integrals { get; set; } = new double[2];
        public double[] Derivatieves { get; set; } = new double[4];

        [DllImport("..\\..\\..\\..\\x64\\Debug\\Dll.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern double InterpolateMKL(int length_nonuni, int length_uni, double[] points, double[] func, double[] res);
        [DllImport("..\\..\\..\\..\\x64\\Debug\\Dll.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern double IntegrateMKL(int length_nonuni, double[] points, double[] func, double[] limits, double[] res);

        public SplinesData(MeasuredData md, SplineParameters sp)
        {
            Measures = md;
            Params = sp;
        }

        public double Interpolate()
        {
            double[] interpolation = new double[3 * Params.LengthUni];

            double error_val = InterpolateMKL(Measures.Length, Params.LengthUni, Measures.Grid, Measures.Values, interpolation);
            if (error_val == 0)
            {
                double[] resault = new double[Params.LengthUni];
                for (int i = 0; i < Params.LengthUni; i++)
                {
                    resault[i] = interpolation[0 + (3 * i)];
                }
                Splines = resault;

                Derivatieves[0] = interpolation[1];
                Derivatieves[1] = interpolation[(3 * Params.LengthUni) - 2];
                Derivatieves[2] = interpolation[2];
                Derivatieves[3] = interpolation[(3 * Params.LengthUni) - 1];
                return 0;
            }
            else
            {
                return error_val;
            }
        }

        public double Integrate()
        {
            double[] Integral = new double[Measures.Length];

            double error_val = IntegrateMKL(Measures.Length, Measures.Grid, Measures.Values, new double[] { Params.x1, Params.x2 }, Integral);
            if (error_val == 0)
            {
                Integrals[0] = Integral.Sum();

                error_val = IntegrateMKL(Measures.Length, Measures.Grid, Measures.Values, new double[] { Params.x2, Params.x3 }, Integral);
                if (error_val == 0)
                {
                    Integrals[1] = Integral.Sum();
                    return 0;
                }
                else
                {
                    return error_val;
                }
            }
            else
            {
                return error_val;
            }
        }
    }
}
