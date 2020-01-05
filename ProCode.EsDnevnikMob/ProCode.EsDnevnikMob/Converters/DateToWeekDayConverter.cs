using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProCode.EsDnevnikMob.Converters
{
    class DateToWeekDayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string valueStr)
            {
                DateTime.TryParse(valueStr, out DateTime result);
                switch(result.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        return "понедељак";
                    case DayOfWeek.Tuesday:
                        return "уторак";
                    case DayOfWeek.Wednesday:
                        return "среда";
                    case DayOfWeek.Thursday:
                        return "четвртак";
                    case DayOfWeek.Friday:
                        return "петак";
                    case DayOfWeek.Saturday:
                        return "субота";
                    case DayOfWeek.Sunday:
                        return "недеља";
                    default:
                        return DateTimeFormatInfo.CurrentInfo.GetDayName(result.DayOfWeek);
                }
                
            }
            else
                return value;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
