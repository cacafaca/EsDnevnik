using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProCode.EsDnevnikMob.DataTemplates
{
    public class TimeLineDataTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var timeLineEvent = item as EsDnevnik.Model.GeneratedTimeLine.TimeLineEvent;
            if (timeLineEvent.Type == EsDnevnik.Model.GeneratedTimeLine.EventType.Absent)
                return timeLineAbsenceDataTemplate;
            else
                return timeLineGradeDataTemplate;
        }
        private DataTemplate timeLineAbsenceDataTemplate = new DataTemplate(typeof(TimeLineAbsenceViewCell));
        private DataTemplate timeLineGradeDataTemplate = new DataTemplate(typeof(TimeLineGradeViewCell));
    }
}
