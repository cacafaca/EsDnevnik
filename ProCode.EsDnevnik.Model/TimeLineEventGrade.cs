using System;
using System.Collections.Generic;
using System.Text;

namespace ProCode.EsDnevnik.Model
{
    public class TimeLineEventGrade : TimeLineEvent
    {
        public Grade Grade { get; set; }
        public override string ToString()
        {
            return $"{Date}: Upisana {Grade.Value} ({Grade.Name}) iz predmeta {Course}.";
        }

        protected override string GetSummary()
        {
            return $"Uneta ocena {FullGrade} dana {Date} iz predmeta {Course}.";
        }

        public string FullGrade { get; set; }
        public string GradeCategory { get; set; }
        public string Note { get; set; }
    }
}
