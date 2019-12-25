using System;
using System.Collections.Generic;
using System.Text;

namespace ProCode.EsDnevnik.Model
{
    public class TimeLineEventFinalGrade : TimeLineEvent
    {
        public TimeLineEventFinalGrade()
        {
            Grade = new Grade();
        }
        protected override string GetSummary()
        {
            return $"Uneta ocena {Grade.Value} dana {Date} iz predmeta {Course}.";
        }
        public Grade Grade { get; set; }
        public string Engagement { get; set; }
        public string SchoolyearPart { get; set; }
    }
}
