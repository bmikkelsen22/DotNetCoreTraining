using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Models
{
    public class PersonDepartment
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
