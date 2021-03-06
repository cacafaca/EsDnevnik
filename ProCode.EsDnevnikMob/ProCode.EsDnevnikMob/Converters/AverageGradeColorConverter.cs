﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProCode.EsDnevnikMob.Converters
{
    class AverageGradeLightColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = Color.FromHex("#fafafa");   // light gray
            if (value is float grade && grade > 0)
                if (grade < 1.5f)
                    color = Color.FromHex("#ffebee");   // light red
                else if (grade < 2.5f)
                    color = Color.FromHex("#fff3e0");   // light orange
                else if (grade < 3.5f)
                    color = Color.FromHex("#f3e5f5");   // light purple
                else if (grade < 4.5f)
                    color = Color.FromHex("#e3f2fd");   // light blue
                else
                    color = Color.FromHex("#e8f5e9");   // light green

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
