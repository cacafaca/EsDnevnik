using System;
using System.Collections.Generic;
using System.Text;

namespace ProCode.EsDnevnik.Model
{
    public class TimeLineEventAbsent : TimeLineEvent
    {
        public byte SchoolHour { get; set; }
        public string WorkHourNote { get; set; }
        public string TeacherNote { get; set; }
        public string ClassMasterNote { get; set; }
        public string AbsentType { get; set; }
        public string Status { get; set; }
        public byte StatusId { get; set; }

        protected override string GetSummary()
        {
            return $"Izostanak dana {Date}. Čas {SchoolHour}. {Course}. Status {Status}.";
        }
    }
}
