using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace CoBCCanteen.Views.Converters
{
	public class CardNumberConverter : IValueConverter
	{
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            StringBuilder sb = new StringBuilder(Regex.Replace(value.ToString(), @"\D", ""));

            foreach (var i in Enumerable.Range(0, sb.Length / 4).Reverse())
            {
                sb.Insert(4 * i + 4, " ");
            }
                
            return sb.ToString().Trim();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Regex.Replace(value.ToString(), @"\D", "");
        }
    }
}

