using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Models
{
    public class Department
    {
        public Department()
        {
            this.PersonDepartments = new HashSet<PersonDepartment>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        public virtual ICollection<PersonDepartment> PersonDepartments { get; set; }
    }
}
