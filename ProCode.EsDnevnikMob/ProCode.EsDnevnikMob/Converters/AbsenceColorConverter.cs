using ProCode.EsDnevnik.Model.GeneratedTimeLine;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProCode.EsDnevnikMob.Converters
{
    class AbsenceColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = Color.Gray;
            if(value is AbsenceStatusIdType statusIdType)
                switch(statusIdType)
                {
                    case AbsenceStatusIdType.NotJustified:
                        color = Color.Red;
                        break;
                    case AbsenceStatusIdType.Justified:
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
