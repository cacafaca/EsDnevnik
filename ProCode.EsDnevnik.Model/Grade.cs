using System;
using System.Collections.Generic;
using System.Text;

namespace ProCode.EsDnevnik.Model
{
    public class Grade
    {
        public int Id { get; set; }
        public int GradeTypeId { get; set; }
        public string Name { get; set; }
        public byte Value { get; set; }
        public int Sequence { get; set; }
        public Grade()
        {
            Name = string.Empty;
        }
    }
}
