using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace CoBCCanteen.Views.Converters
{
	public class ExpiryDateConverter : IValueConverter
	{
		public ExpiryDateConverter()
		{
		}

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            StringBuilder sb = new StringBuilder(Regex.Replace(value.ToString(), @"\D", ""));

            foreach (var i in Enumerable.Range(0, sb.Length / 2).Reverse())
            {
                sb.Insert(2 * i + 2, "/");
            }

            if (sb.Length == 6)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString().Trim();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Regex.Replace(value.ToString(), @"\D", "");
        }
    }
}

