using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace Lab2Var2
{
    class IntConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                int val = (int)value;
                return $"{val}";
            }
            catch (Exception error)
            {
                MessageBox.Show($"Unexpected error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return "ERROR!";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return value != null ? int.Parse(value as string) : 0;
            }
            catch (Exception error)
            {
                MessageBox.Show($"Unexpected error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
        }
    }
}
