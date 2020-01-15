using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProCode.EsDnevnikMob.Converters
{
    class GradeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int grade = 0;
            Color color = Color.Gray;
            if (value is int gradeInt)
                grade = gradeInt;
            else if (value is string gradeStr && int.TryParse(gradeStr, out gradeInt))
                if (gradeInt > 0)
                    grade = gradeInt;

            switch (grade)
            {
                case 1:
                    color = Color.Red;
                    break;
                case 2:
                    color = Color.Orange;
                    break;
                case 3:
                    color = Color.Purple;
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
