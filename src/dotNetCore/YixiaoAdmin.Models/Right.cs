using System;
using System.Collections.Generic;
using System.Text;

namespace YixiaoAdmin.Models
{
    public class Right:Entity
    {
        public Right()
        {

        }
        public string Code { get; set; }

        public List<RoleRight> RoleRights { get; set; }
    }
}
