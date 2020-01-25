using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProCode.EsDnevnik.Model.GeneratedAbsences
{

    public class Rootobject : Dictionary<string, Sequence>
    {
    }

    public class Sequence
    {
        public int ClassCourseId { get; set; }
        public string Name { get; set; }
        [JsonProperty("sequence")]
        public string SequenceName { get; set; }
        public AbsentStatuses AbsentStatuses { get; set; }
    }

    public class AbsentStatuses
    {
        [JsonProperty("2")]
        public AbsentStatus AbsentStatusId2 { get; set; }
    }

    public class AbsentStatus
    {
        public int StatusId { get; set; }
        public string Name { get; set; }
        public Absent[] Absents { get; set; }
    }

    public class Absent
    {
        public int Id { get; set; }
        public int WorkHourId { get; set; }
        public object TeacherNote { get; set; }
        public string StatusName { get; set; }
        public string WorkHourNote { get; set; }
        public string WorkdayDate { get; set; }
    }
}
