using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Models
{
    public class PersonRole
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
        
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
