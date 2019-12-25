using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProCode.EsDnevnik.Model
{
    public class School
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public IList<SchoolYear> SchoolYears { get; set; }
        public School()
        {
            SchoolYears = new List<SchoolYear>();
        }
        public SchoolYear GetLastSchoolYear()
        {
            return SchoolYears.OrderByDescending(schoolYear => schoolYear.Year).FirstOrDefault();
        }
        public Class GetCurrentClass()
        {
            return GetLastSchoolYear().Classes.OrderByDescending(cl => cl.RecordId).FirstOrDefault();
        }
    }
}
