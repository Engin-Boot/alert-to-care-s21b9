using System;
using System.Globalization;
using System.Windows.Data;

namespace AlertToCareFrontend.ViewModels.Converters
{
    public class ChangeSpo2AlarmStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                int spo2 = Int32.Parse(value.ToString());
                if (spo2 < 95)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception) { return false; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
