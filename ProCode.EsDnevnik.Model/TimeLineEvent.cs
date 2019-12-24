using System;
using System.Collections.Generic;
using System.Text;

namespace ProCode.EsDnevnik.Model
{
    public abstract class TimeLineEvent
    {
        public TimeLineEventType Type { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreateTime { get; set; }
        public string Course { get; set; }
        public int ClassCourseId { get; set; }
        public string SchoolClass { get; set; }
        public string School { get; set; }
        public string Summary { get { return GetSummary(); } }
        protected abstract string GetSummary();
    }
}
