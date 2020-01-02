using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProCode.EsDnevnikMob.Converters
{
    public class ItemAppearingEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ItemVisibilityEventArgs itemVisibilityEventArgs))
            {
                throw new ArgumentException("Expected value to be of type ItemVisibilityEventArgs.", nameof(value));
            }
            return itemVisibilityEventArgs.Item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
