using ProCode.EsDnevnik.Model.GeneratedTimeLine;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProCode.EsDnevnikMob.Converters
{
    class GradeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Grade grade)
                if (grade.GradeTypeId != null && grade.GradeTypeId == 1)
                    return grade.Value;
                else
                    return grade.Name;
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
