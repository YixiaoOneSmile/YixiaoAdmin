/****************************************************
 * 本文件由T4模板生成，请将本文件复制到YixiaoAdmin.Services类库中使用
 * 文件名：RoleServices.cs
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

    public partial class  RoleServices:BaseServices<Role>, IRoleServices
    {
       public async Task<PagesResponse> QueryPagesExpand(QueryPageModel queryPageModel)
        {
             //自定义分页Response
            PagesResponse pagesResponse = new PagesResponse();
            //初始化查询表达式
            Expression<Func<Role, bool>> whereExpression = PredicateBuilder.True<Role>();

            foreach (QueryFieldModel item in queryPageModel.Query)
            {
                //根据属性名获取属性
                var property = typeof(Role).GetProperty(item.QueryField);
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
            var query = await _RoleRepository.Query(whereExpression, queryPageModel.Orderby, queryPageModel.CurrentPage, queryPageModel.PageNumber);

            //添加联合查询条件
            query = query.Include(x => x.RoleRights).ThenInclude(x=>x.Right);

            pagesResponse.Success(query.ToList());
            pagesResponse.count = (await _RoleRepository.Query(whereExpression)).Count();
            return pagesResponse;
           
        }

       public async Task<bool> AddExpand(Role role)
        {
            InitModel.Init(role, "", true);

            foreach(var item in role.RoleRights)
            {
                InitModel.Init(item, "", true);
                item.RoleId = role.Id;
            }

            return await _RoleRepository.AddAsync(role);
        }

        public async Task<bool> UpdateExpand(Role role)
        {
            return await _RoleRepository.UpdateExpand(role);
           
        }
    }
}
