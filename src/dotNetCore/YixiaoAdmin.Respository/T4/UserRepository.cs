/****************************************************
 * 本文件由T4模板生成，重新生成T4模板后会导致代码丢失
 * 如需修改请使用partial关键词
 * 文件名：UserRespository.cs
 * 生成时间：01/11/2023 22:48:29
****************************************************/
using System;
using System.Linq;
using YixiaoAdmin.IRepository;
using YixiaoAdmin.Models;
using YixiaoAdmin.EntityFrameworkCore;
namespace YixiaoAdmin.Repository
{

    public partial class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(YixiaoAdminContext db):base(db)
        {
        }
    }
}
