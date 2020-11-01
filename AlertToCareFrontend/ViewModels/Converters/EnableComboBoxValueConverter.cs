using System;
using System.Globalization;
using System.Windows.Data;

namespace AlertToCareFrontend.ViewModels.Converters
{
    public class EnableComboBoxValueConverter : IValueConverter
    {
        // Source -> Target
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value?.ToString() == "True")
                return false;
            return true;
        }

        // Target -> Source
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value?.ToString() == "True")
                return false;
            return true;
        }
    }
}
