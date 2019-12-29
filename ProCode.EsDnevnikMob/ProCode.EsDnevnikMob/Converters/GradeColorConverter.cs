using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProCode.EsDnevnikMob
{
    class GradeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = Color.Aqua;
            if(value is int grade)
                switch(grade)
                {
                    case 1:
                        color = Color.Red;
                        break;
                    case 2:
                        color = Color.Orange;
                        break;
                    case 3:
                        color = Color.Yellow;
                        break;
                    case 4:
                        color = Color.Blue;
                        break;
                    case 5:
                        color = Color.Green;
                        break;
                    default:
                        color = Color.Gray;
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
