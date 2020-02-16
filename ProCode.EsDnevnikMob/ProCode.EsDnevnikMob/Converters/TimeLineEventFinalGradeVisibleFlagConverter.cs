using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProCode.EsDnevnikMob.Converters
{
    class TimeLineEventFinalGradeVisibleFlagConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool visible = false;
            if(value is ProCode.EsDnevnik.Model.GeneratedTimeLine.TimeLineEvent timeLineEvent)
            {
                visible = timeLineEvent.Type == EsDnevnik.Model.GeneratedTimeLine.EventType.FinalGrade;
            }
            return visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
