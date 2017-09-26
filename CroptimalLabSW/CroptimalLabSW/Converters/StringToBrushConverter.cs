using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace CroptimalLabSW.Converters
{
    public class StringToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string brushName = value.ToString();
            switch (brushName)
            {
                case "Black":
                    {
                        return Brushes.Black;
                    }
                case "Red":
                    {
                        return Brushes.Red;
                    }
                case "Green":
                    {
                        return Brushes.Green;
                    }
                case "DarkBrush":
                    {
                        return Brushes.DarkGray;
                    }
                default:
                    {
                        return Brushes.Black;
                    }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
