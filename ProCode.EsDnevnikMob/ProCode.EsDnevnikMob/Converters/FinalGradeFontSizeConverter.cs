using ProCode.EsDnevnik.Model.GeneratedTimeLine;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProCode.EsDnevnikMob.Converters
{
    class FinalGradeFontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string finalGrade)
                if (int.TryParse(finalGrade, out int finalGradeValue) && finalGradeValue > 0)
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
