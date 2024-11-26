using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Labb_3_CSharp.Model
{
    class DifficultyConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum enumValue)
            {
                return System.Convert.ToInt32(value);
            }
            throw new ArgumentException("Value must be an Enum.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (targetType.IsEnum)
            {
                if (value is int intValue) 
                {
                    return Enum.ToObject(targetType, intValue);
                }
                else if (value is string stringValue) 
                {
                     if (Enum.TryParse(targetType, stringValue, out var result))
                    {
                        return result;
                    }
                }
            }
            throw new ArgumentException("Invalid value or parameter type.");
        }
    }
}
