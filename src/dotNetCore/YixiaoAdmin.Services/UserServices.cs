/****************************************************
 * 本文件由T4模板生成，请将本文件复制到YixiaoAdmin.Services类库中使用
 * 文件名：UserServices.cs
 * 生成时间：12/22/2020 21:29:43
****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YixiaoAdmin.IRepository;
using YixiaoAdmin.IServices;
using YixiaoAdmin.Models;
using YixiaoAdmin.Models.ViewModels;
using YixiaoAdmin.Common;
using Microsoft.EntityFrameworkCore;

namespace YixiaoAdmin.Services
{

    public partial class UserServices : BaseServices<User>, IUserServices
    {
        public async Task<PagesResponse> QueryPagesExpand(QueryPageModel queryPageModel)
        {
            //自定义分页Response
            PagesResponse pagesResponse = new PagesResponse();
            //初始化查询表达式
            Expression<Func<User, bool>> whereExpression = PredicateBuilder.True<User>();

            foreach (QueryFieldModel item in queryPageModel.Query)
            {
                //根据属性名获取属性
                var property = typeof(User).GetProperty(item.QueryField);
                if (property == null)
                {
                    continue;
                }
                if (item.QueryStr == null || item.QueryStr == "")
                {
                    continue;
                }
                if (item.QueryField == "Name")
                {

                    whereExpression = PredicateBuilder.And(whereExpression, (x) => x.Name == item.QueryStr);
                }

                else if (item.QueryField == "CreateTime")
                {
                    whereExpression = PredicateBuilder.And(whereExpression, (x) => x.CreateTime.Date == Convert.ToDateTime(item.QueryField.Trim()));
                }

            }
            //获取查询语句
            var query = await _UserRepository.Query(whereExpression, queryPageModel.Orderby, queryPageModel.CurrentPage, queryPageModel.PageNumber);

            //添加联合查询条件
            query = query.Include(x => x.Role).ThenInclude(x=>x.RoleRights).ThenInclude(x => x.Right);

            pagesResponse.Success(query.ToList());
            pagesResponse.count = (await _UserRepository.Query(whereExpression)).Count();
            return pagesResponse;

        }

        public async Task<Response> Login(string Username, string Password)
        {
            Response response = new Response();
            User user = (await _UserRepository.Query(x => x.UserName == Username && x.Password == Password)).Include(x => x.Role).ThenInclude(x => x.RoleRights).ThenInclude(x => x.Right).FirstOrDefault();
            if (user != null)
            {
                User userView = new User();

                userView.Role = user.Role;
                userView.Name = user.Name;
                response.Success(user);
            }
            else
            {
                response.ItemNotFound();
            }
            return response;
        }
    }
}
