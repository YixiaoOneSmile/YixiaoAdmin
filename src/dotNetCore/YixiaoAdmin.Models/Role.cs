using System;
using System.Collections.Generic;
using System.Text;

namespace YixiaoAdmin.Models
{
    public class Role:Entity
    {
        public Role()
        {

        }
        public string Code { get; set; }

        public List<User> Users { get; set; }

        public List<RoleRight> RoleRights { get; set; }
    }
}
