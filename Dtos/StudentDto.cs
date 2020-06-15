using Lambdatech.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lambdatech.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MomId { get; set; }
        public string MomName { get; set; }
        public int DadId { get; set; }
        public string DadName { get; set; }
        public List<SchoolDto> Schools { get; set; }
}
}
