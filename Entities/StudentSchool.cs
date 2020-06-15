using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lambdatech.Entities
{
    public class StudentSchool
    {
        public int StudentId { get; set; }
        public int SchoolId { get; set; }
        public Student Student { get; set; }
        public School School { get; set; }
    }
}
