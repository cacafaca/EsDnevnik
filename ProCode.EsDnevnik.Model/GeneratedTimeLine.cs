using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ProCode.EsDnevnik.Model.GeneratedTimeLine
{
    public class Rootobject
    {
        public Dictionary<string, TimeLineEvent[]> Data { get; set; }
        public Meta Meta { get; set; }
    }

    public class TimeLineEvent
    {
        public EventType Type { get; set; }
        public string Date { get; set; }
        public string CreateTime { get; set; }
        public string FullGrade { get; set; }
        public Grade Grade { get; set; }
        public string GradeCategory { get; set; }
        public string Note { get; set; }
        public string Course { get; set; }
        public int ClassCourseId { get; set; }
        public string SchoolClass { get; set; }
        public string School { get; set; }
        public object IopNote { get; set; }
        public object EvaluationElementCourse { get; set; }
        public int SchoolHour { get; set; }

        [JsonIgnore]
        public string Summary
        {
            get
            {
                string summary;
                if (Type == EventType.Absent)
                {
                    summary = $"Izostanak dana {GetDate().ToShortDateString()} sa predmeta {Course}. Broj časa {SchoolHour}";
                }
                else
                {
                    summary = $"Uneta ocena {FullGrade} iz predmeta {Course} dana {GetDate().ToShortDateString()}.";
                }
                return summary;
            }
        }

        protected DateTime DateCache { get; set; }

        public DateTime GetDate()
        {
            if (DateCache == DateTime.MinValue)
                return DateCache = DateTime.ParseExact(Date, "yyyy-MM-dd", null );
            else
                return DateCache;
        }

        public override string ToString()
        {
            return Summary;
        }
    }

    public class Grade
    {
        public int Id { get; set; }
        [JsonProperty("grade_type_id")]
        public int GradeTypeId { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int Sequence { get; set; }
    }

    public class Meta
    {
        public int Total { get; set; }
    }

    public enum EventType
    {
        [EnumMember(Value = "grade")]
        Grade,
        [EnumMember(Value = "absent")]
        Absent,
        [EnumMember(Value = "final-grade")]
        FinalGrade
    }
}
