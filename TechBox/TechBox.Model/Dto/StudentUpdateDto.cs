using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBox.Model.Dto
{
    public class StudentUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CurrentGradeId { get; set; }
    }
}
