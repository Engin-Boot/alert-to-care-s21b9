using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace AlertToCareFrontend.ViewModels.Converters
{
    public class ChangeRespRateAlarmStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double respRate = Double.Parse(value.ToString());
            if (respRate < 12)
                return true;
            if (respRate > 16)
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
