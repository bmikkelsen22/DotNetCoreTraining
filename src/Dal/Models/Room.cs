using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Models
{
    public class Room
    {
        public Room()
        {
            this.Departments = new HashSet<Department>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        // TODO: Add Department models
        public virtual ICollection<Department> Departments { get; set; }
    }
}
