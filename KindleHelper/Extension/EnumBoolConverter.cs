using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KindleHelper.Extension
{
    public class EnumBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string parameterStr = parameter as string;
            if (parameterStr == null)
            {
                return Binding.DoNothing;
            }
            if (Enum.IsDefined(value.GetType(),value) == false) 
                return Binding.DoNothing;
            object parameterValue = Enum.Parse(value.GetType(), parameterStr);
            return parameterValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string parameterString = parameter as string;
            if (parameterString == null)
                return Binding.DoNothing;

            return Enum.Parse(targetType, parameterString);
        }
    }
}
