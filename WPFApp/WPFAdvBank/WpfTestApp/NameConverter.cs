using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfTestApp
{
    public class NameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
                string name;

                switch ((string)parameter)
                {
                    case "FormatLastFirst":
                        name = $"{values[1]}, {values[0]}";
                        break;
                    default:
                        name = $"{values[0]}, {values[1]}";
                        break;
                }
            return name;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var splitValues = ((string)value).Split(' ');
            return splitValues;
        }
    }
}
