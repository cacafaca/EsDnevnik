using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProCode.EsDnevnikMob.Converters
{
    class AverageGradeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = Color.Gray;
            if (value is float grade && grade > 0)
                if (grade < 1.5f)
                    color = Color.Red;
                else if (grade < 2.5f)
                    color = Color.Orange;
                else if (grade < 3.5f)
                    color = Color.Purple;
                else if (grade < 4.5f)
                    color = Color.Blue;
                else
                    color = Color.Green;

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
