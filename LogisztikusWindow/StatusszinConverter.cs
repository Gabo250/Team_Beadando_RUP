using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace LogisztikusWindow
{
    class StatusszinConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Allapot al = (Allapot)value;
            if (al == Allapot.Operatorra_var)
            {
                return Brushes.Red;
            }
            else if (al == Allapot.Folyamatban)
            {
                return Brushes.Blue;
            }
            else
            {
                return Brushes.Green;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
