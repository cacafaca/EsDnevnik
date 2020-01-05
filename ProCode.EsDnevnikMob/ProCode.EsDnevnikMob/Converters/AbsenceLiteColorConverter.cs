using ProCode.EsDnevnik.Model.GeneratedTimeLine;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProCode.EsDnevnikMob.Converters
{
    class AbsenceLiteColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = Color.Gray;
            if(value is AbsenceStatusIdType statusIdType)
                switch(statusIdType)
                {
                    case AbsenceStatusIdType.NotJustified:
                        color = Color.FromHex("#ffebee");   // light red
                        break;
                    case AbsenceStatusIdType.Justified:
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
