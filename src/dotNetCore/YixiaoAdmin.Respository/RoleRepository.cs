using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YixiaoAdmin.Common;
using YixiaoAdmin.Models;

namespace YixiaoAdmin.Repository
{
    public partial class RoleRepository
    {
        public async Task<bool> UpdateExpand(Role model)
        {
            Role role = db.Role.Where(x=>x.Id==model.Id).Include(x=>x.RoleRights).ThenInclude(x=>x.Right).FirstOrDefault();

            role.Name = model.Name;
            role.Code = model.Code;
            foreach (var item in model.RoleRights)
            {
                if (item.Id == null)
                {
                    item.Right = null;
                    InitModel.Init(item, "", true);
                    role.RoleRights.Add(item);
                }
            }
            foreach (var item in role.RoleRights)
            {
                var roleRight = model.RoleRights.FirstOrDefault(x => x.Id == item.Id);
                if (roleRight == null)
                {
                    db.RoleRight.Remove(item);
                }
            }


            var result = await db.SaveChangesAsync();
            if (result == 0)
            {
                return false;
            }
            return true;
        }

    }
}
