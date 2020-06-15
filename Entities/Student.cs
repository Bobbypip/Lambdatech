using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lambdatech.Entities
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int MomId { get; set; }
        public Mom Mom { get; set; }
        public int DadId { get; set; }
        public Dad Dad { get; set; }
        public List<School> Schools { get; set; }
    }
}
