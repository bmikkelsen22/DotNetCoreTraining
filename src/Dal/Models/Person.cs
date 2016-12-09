using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Models
{
    public class Person
    {
        public Person()
        {
            this.PersonDepartments = new HashSet<PersonDepartment>();
            //this.Roles = new HashSet<Role>(); //unused, only 1 many to many
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PersonDepartment> PersonDepartments { get; set; }
        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }
        //public virtual ICollection<Role> Roles { get; set; }
    }
}
