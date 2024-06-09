using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YixiaoAdmin.Models;

namespace YixiaoAdmin.IRepository
{
    public partial interface IRoleRepository 
    {
        Task<bool> UpdateExpand(Role role);
    }
}
