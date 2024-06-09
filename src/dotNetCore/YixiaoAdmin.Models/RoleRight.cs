using System;
using System.Collections.Generic;
using System.Text;

namespace YixiaoAdmin.Models
{
    public class RoleRight:Entity
    {
        public string RoleId { get; set; }

        public Role Role { get; set; }

        public string  RightId{ get; set; }

        public Right Right { get; set; }
    }
}
