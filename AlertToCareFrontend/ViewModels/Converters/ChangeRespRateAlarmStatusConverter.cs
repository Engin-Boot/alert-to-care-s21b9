using System;
using System.Globalization;
using System.Windows.Data;

namespace AlertToCareFrontend.ViewModels.Converters
{
    public class ChangeRespRateAlarmStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double respRate = Double.Parse(value?.ToString() ?? string.Empty);
                if (respRate < 12||respRate>16)
                    return true;
              
                return false;
            }
            catch (Exception) { return false; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
