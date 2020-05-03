using ProCode.EsDnevnik.Model.GeneratedTimeLine;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProCode.EsDnevnikMob.Converters
{
    class TimeLineEventActivityTypeConverter
        : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string activityTypeSign;
            if (value is ActivityType activityType)
                switch (activityType)
                {
                    case ActivityType.NotSatisfying:
                        activityTypeSign = "\uf119";
                        break;
                    case ActivityType.Satisfy:
                        activityTypeSign = "\uf11a";
                        break;
                    case ActivityType.Successful:
                        activityTypeSign = "\uf118";
                        break;
                    default:
                        activityTypeSign = "?";
                        break;
                }
            else
                activityTypeSign = "?";
            return activityTypeSign;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
