using System;
using System.Collections.Generic;
using System.Text;

namespace YixiaoAdmin.Models
{
    public class User :Entity
    {
        public User()
        {

        }
        public string UserName { get; set; }

        public string Password { get; set; }

        public string RoleId { get; set; }
        public Role Role { get; set; }

      
    }
}
