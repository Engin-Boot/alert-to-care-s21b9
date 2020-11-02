using System;
using System.Globalization;
using System.Windows.Data;

namespace AlertToCareFrontend.ViewModels.Converters
{
    public class ChangeBpAlarmStatusConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double bpm = Double.Parse(value?.ToString() ?? string.Empty);

                if (bpm < 70)
                    return true;
                if (bpm > 100)
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
