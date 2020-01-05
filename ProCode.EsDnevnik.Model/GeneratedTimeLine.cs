using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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

        public Rootobject()
        {
            Data = new Dictionary<string, TimeLineEvent[]>();
            Meta = new Meta();
        }
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
        public string WorkHourNote { get; set; }
        public string TeacherNote { get; set; }
        public string ClassmasterNote { get; set; }
        public string AbsentType { get; set; }
        public string Status { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public AbsenceStatusIdType StatusId { get; set; }

        [JsonIgnore]
        public string Summary
        {
            get
            {
                string summary;
                switch (Type)
                {
                    case EventType.Absent:
                        summary = $"Изостанак дана {GetDate().ToString("dd.MM.yy")} са предмета {Course}. Број часа {SchoolHour}. Статус: {Status}.";
                        break;
                    case EventType.Grade:
                        summary = $"Унета оцена {FullGrade} из предмета {Course} дана {GetDate().ToString("dd.MM.yy")}.";
                        break;
                    case EventType.FinalGrade:
                        summary = $"Закључена оцена {FullGrade} из предмета {Course} дана {GetDate().ToString("dd.MM.yy")}.";
                        break;
                    default:
                        summary = string.Empty;
                        break;
                }
                return summary;
            }
        }
        [JsonIgnore]
        public bool IsLoading { get { return Type == EventType.Loading; } }

        protected DateTime DateCache { get; set; }

        public DateTime GetDate()
        {
            if (DateCache == DateTime.MinValue)
                return DateCache = DateTime.ParseExact(Date, "yyyy-MM-dd", null);
            else
                return DateCache;
        }

        public override string ToString()
        {
            return Summary;
        }

        /// <summary>
        /// Flag for new event since last update.
        /// </summary>
        public bool IsNew { get; set; }
    }

    public class Grade
    {
        public int Id { get; set; }
        [JsonProperty("grade_type_id")]
        public int GradeTypeId { get; set; }    // 1=brojevna ocena, 2=opisna
        public string Name { get; set; }
        public int Value { get; set; }
        public int Sequence { get; set; }
        public override string ToString()
        {
            return $"Ocena: {Value}";
        }
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
        FinalGrade,
        [JsonIgnore]
        Loading
    }

    public enum AbsenceStatusIdType
    {
        [EnumMember(Value = "")]
        Unknown,
        [EnumMember(Value = "1")]
        NotJustified,
        [EnumMember(Value = "2")]
        Justified
    }
}
