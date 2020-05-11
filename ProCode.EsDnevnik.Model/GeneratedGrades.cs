using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProCode.EsDnevnik.Model.GeneratedGrades
{
    public class Rootobject
    {
        public CourseGrades[] Courses { get; set; }
    }

    public class CourseGrades
    {
        public string Course { get; set; }
        public int ClassCourseId { get; set; }
        public int ClassCourseGradeTypeId { get; set; }
        public int Sequence { get; set; }
        public Parts Parts { get; set; }
        [JsonIgnore]
        public string Final
        {
            get
            {
                return Parts.Part2.Final != null ? Parts.Part2.Final.Value.ToString() :
                    Parts.Part1.Final != null ? Parts.Part1.Final.Value.ToString() : string.Empty;
            }
        }

        public override string ToString()
        {
            return $"Предмет:{Course}, <Прво полугође:{Parts.Part1?.GradesString}, Просек:{Parts.Part1?.Average}, Закључна:{Parts.Part1?.Final?.Value}>, <Друго полугође:{Parts.Part2?.GradesString}, Просек:{Parts.Part2?.Average}, Закључна:{Parts.Part2?.Final?.Value}>";
        }

        /// <summary>
        /// Calculated average value. Not taken from data.
        /// </summary>
        [JsonIgnore]
        public float AverageCalculated
        {
            get
            {
                if (Parts != null)
                {
                    if (Parts.Part1 != null && Parts.Part1.Grades != null)
                    {
                        var part1 = Parts.Part1.Grades.Where(g => g.GradeValue > 0).Select(g => g.GradeValue);
                        if (part1.Count() > 0)
                        {
                            if (Parts.Part2 != null && Parts.Part2.Grades != null)
                            {
                                var part2 = Parts.Part2.Grades.Where(g => g.GradeValue > 0).Select(g => g.GradeValue);
                                if (part2.Count() > 0)
                                    return ((float)(part1.Sum() + part2.Sum())) / (part1.Count() + part2.Count());
                                else
                                    return (float)part1.Sum() / part1.Count();
                            }
                            else
                                return (float)part1.Sum() / part1.Count();
                        }
                        else
                            return 0;
                    }
                    else
                        return 0;
                }
                else
                    return 0;
            }
        }

        [JsonIgnore]
        public bool IsNumerical { get { return ClassCourseGradeTypeId == 1; } }
    }

    public class Parts
    {
        // Semester 1
        [JsonProperty("1")]
        public Part Part1 { get; set; }

        // Semester 1
        [JsonProperty("2")]
        public Part Part2 { get; set; }
    }

    // Polugođe
    public class Part
    {
        public Grade[] Grades { get; set; }
        public Final Final { get; set; }
        public float Average { get; set; }
        [JsonIgnore]
        public string GradesString
        {
            get
            {
                return string.Join(", ", Grades.Where(g => g.GradeValue > 0).Select(g => g.GradeValue).ToArray());
            }
        }
    }

    public class Grade
    {
        public bool Descriptive { get; set; }
        public string Date { get; set; }
        public string CreateTime { get; set; }
        public string FullGrade { get; set; }
        [JsonProperty("grade")]
        public int GradeValue { get; set; }
        public string GradeCategory { get; set; }
        public string Note { get; set; }
        public object SchoolyearPartId { get; set; }
        public object EvaluationElement { get; set; }
    }

    public class Final
    {
        public string Name { get; set; }
        public int Value { get; set; }
        [JsonProperty("schoolyear_part_id")]
        public int SchoolYearPartId { get; set; }
        public object Engagement { get; set; }
    }
}
