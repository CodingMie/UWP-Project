using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ShowMeMyMoney.Converters
{
    public class DateDisplayCvt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTimeOffset d = (DateTimeOffset)value;
            string s = d.Year + "年" + d.Month + "月" + d.Day + "日";
            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
