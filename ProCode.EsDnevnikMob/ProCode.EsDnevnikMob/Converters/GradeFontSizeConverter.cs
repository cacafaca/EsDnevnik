using ProCode.EsDnevnik.Model.GeneratedTimeLine;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProCode.EsDnevnikMob.Converters
{
    class GradeFontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Grade grade)
                if (grade.GradeTypeId != null && grade.GradeTypeId == 1)
                    return 50;  // size for number grade
                else
                    return 20;  // size for descriptive grade
            else
                return 50;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
