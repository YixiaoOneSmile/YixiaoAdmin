/****************************************************
 * 本文件由T4模板生成，重新生成T4模板后会导致代码丢失
 * 如需修改请使用partial关键词
 * 文件名：RoleRespository.cs
****************************************************/
using System;
using System.Linq;
using YixiaoAdmin.IRepository;
using YixiaoAdmin.Models;
using YixiaoAdmin.EntityFrameworkCore;
namespace YixiaoAdmin.Repository
{

    public partial class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(YixiaoAdminContext db):base(db)
        {
        }
    }
}
