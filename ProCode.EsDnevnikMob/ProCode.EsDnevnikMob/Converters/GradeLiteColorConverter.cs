using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProCode.EsDnevnikMob.Converters
{
    class GradeLiteColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = Color.Gray;
            if(value is int grade)
                switch(grade)
                {
                    case 1:
                        color = Color.FromHex("#ffebee");   // light red
                        break;
                    case 2:
                        color = Color.FromHex("#fff3e0");   // light orange
                        break;
                    case 3:
                        color = Color.FromHex("#f3e5f5");   // light purple
                        break;
                    case 4:
                        color = Color.FromHex("#e3f2fd");   // light blue
                        break;
                    case 5:
                        color = Color.FromHex("#e8f5e9");   // light green
                        break;
                    default:
                        color = Color.FromHex("#fafafa");   // light gray
                        break;
                }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
