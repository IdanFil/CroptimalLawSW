using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using CroptimalLabSW.View;

namespace CroptimalLabSW.Converters
{
    public class StringToUserControlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string viewName = value.ToString();
            switch(viewName)
            {
                    case "MenuView":
                    {
                        return new MenuView();
                    }
                    case "ChromameterView":
                    {
                        return new ChromometerView();
                    }
                    default:
                    {
                        return new MenuView();
                    }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
