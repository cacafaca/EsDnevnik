using ProCode.EsDnevnik.Model.GeneratedTimeLine;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProCode.EsDnevnikMob.Converters
{
    class TimeLineEventActivityLiteColorConverter
        : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = Color.Gray;
            if(value is ActivityType activityType)
                switch(activityType)
                {
                    case ActivityType.NotSatisfying:
                        color = Color.FromHex("#ffebee");   // light red = Unsuccessful
                        break;
                    case ActivityType.Satisfy:
                        color = Color.FromHex("#fff3e0");   // light orange = Satisfy
                        break;
                    case ActivityType.Successful:
                        color = Color.FromHex("#e8f5e9");   // light green = Successful
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
