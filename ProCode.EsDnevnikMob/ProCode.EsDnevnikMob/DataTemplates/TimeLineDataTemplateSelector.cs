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
            switch (timeLineEvent.Type)
            {
                case EsDnevnik.Model.GeneratedTimeLine.EventType.Absent:
                    return timeLineAbsenceDataTemplate;
                case EsDnevnik.Model.GeneratedTimeLine.EventType.Grade:
                    return timeLineGradeRegularDataTemplate;
                case EsDnevnik.Model.GeneratedTimeLine.EventType.FinalGrade:
                    return timeLineGradeFinalDataTemplate;
                default:
                    return timeLineLoadingViewCell;
            }
        }
        private readonly DataTemplate timeLineAbsenceDataTemplate = new DataTemplate(typeof(TimeLineAbsenceViewCell));
        private readonly DataTemplate timeLineGradeRegularDataTemplate = new DataTemplate(typeof(TimeLineGradeRegularViewCell));
        private readonly DataTemplate timeLineGradeFinalDataTemplate = new DataTemplate(typeof(TimeLineGradeFinalViewCell));
        private readonly DataTemplate timeLineLoadingViewCell = new DataTemplate(typeof(TimeLineLoadingViewCell));
    }
}
