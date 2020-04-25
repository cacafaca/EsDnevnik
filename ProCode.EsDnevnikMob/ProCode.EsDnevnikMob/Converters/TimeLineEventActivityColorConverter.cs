using ProCode.EsDnevnik.Model.GeneratedTimeLine;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProCode.EsDnevnikMob.Converters
{
    class TimeLineEventActivityColorConverter
        : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = Color.Gray;
            if(value is ActivityType activityType)
                switch(activityType)
                {
                    case ActivityType.Unsuccessful:
                    case ActivityType.NotSatisfy:
                        color = Color.Red;          // light red = Unsuccessful
                        break;
                    case ActivityType.Satisfy:
                        color = Color.Orange;       // light orange = Satisfy
                        break;
                    case ActivityType.Successful:
                        color = Color.Green;        // green = Successful
                        break;
                    default:
                        color = Color.Gray;         // light gray
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
