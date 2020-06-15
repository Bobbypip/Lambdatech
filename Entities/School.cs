using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lambdatech.Entities
{
    public class School
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
