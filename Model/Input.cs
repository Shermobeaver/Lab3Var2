using System;
using System.ComponentModel;

namespace Model
{
    public class Input : IDataErrorInfo
    {
        private int _length;
        public int Length
        {
            get => _length;
            set
            {
                _length = value;
                Error1 = (value < 3);
            }
        }

        private int _lengthUni;
        public int LengthUni
        {
            get => _lengthUni;
            set
            {
                _lengthUni = value;
                Error2 = (value < 3);
            }
        }

        private double _left;
        public double Left
        {
            get => _left;
            set
            {
                _left = value;
                Error1 = (Right < value);
            }
        }
        private double _right;
        public double Right
        {
            get => _right;
            set
            {
                _right = value;
                Error1 = (value < Left);
            }
        }
        private double _x1;
        public double x1
        {
            get => _x1;
            set
            {
                _x1 = value;
                Error2 = ((Left > value) || (value > x2) || (x2 > x3) || (x3 > Right));
            }
        }
        private double _x2;
        public double x2
        {
            get => _x2;
            set
            {
                _x2 = value;
                Error2 = ((Left > x1) || (x1 > value) || (value > x3) || (x3 > Right));
            }
        }
        private double _x3;
        public double x3
        {
            get => _x3;
            set
            {
                _x3 = value;
                Error2 = ((Left > x1) || (x1 > x2) || (x2 > value) || (value > Right));
            }
        }

        public SPf Function { get; set; }
        public bool Error1 { get; set; }
        public bool Error2 { get; set; }

        public Input()
        {
            Length = 25;
            LengthUni = 25 * 10;
            Left = 10;
            Right = 30;
            Function = SPf.Linear;
            x1 = 10;
            x2 = 20;
            x3 = 30;

            Error1 = false;
            Error2 = false;
        }

        public string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Length":
                        if (Length < 3)
                        {
                            error = "Length";
                            Error1 = true;
                        }
                        break;
                    case "Right":
                    case "Left":
                        if (Right < Left)
                        {
                            error = "Borders";
                            Error1 = true;
                        }
                        break;
                    case "LengthUni":
                        if (LengthUni < 3)
                        {
                            error = "UniformLength";
                            Error2 = true;
                        }
                        break;
                    case "x1":
                    case "x2":
                    case "x3":
                        if ((Left > x1) || (x1 > x2) || (x2 > x3) || (x3 > Right))
                        {
                            error = "Limits";
                            Error2 = true;
                        }
                        break;
                    default:
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
