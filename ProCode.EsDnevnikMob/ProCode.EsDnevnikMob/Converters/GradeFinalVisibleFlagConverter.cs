using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProCode.EsDnevnikMob.Converters
{
    class GradeFinalVisibleFlagConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool visible = false;
            if(value is ProCode.EsDnevnik.Model.GeneratedGrades.Part part)
            {
                visible = part.Final != null && part.Final.Value > 0;
            }
            return visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
