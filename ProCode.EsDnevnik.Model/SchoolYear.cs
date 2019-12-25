using System;
using System.Collections.Generic;
using System.Text;

namespace ProCode.EsDnevnik.Model
{
    public class SchoolYear
    {
        public int Id { get; set; }
        /// <summary>
        /// 17/18, 18/19, 19/20, ...
        /// </summary>
        public string Year { get; set; }
        public IList<Class> Classes { get; set; }

        public SchoolYear()
        {
            Classes = new List<Class>();
        }
    }
}
