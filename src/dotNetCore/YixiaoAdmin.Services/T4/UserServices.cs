/****************************************************
 * 本文件由T4模板生成，重新生成T4模板后会导致代码丢失
 * 如需修改请使用partial关键词
 * 文件名：UserServices.cs
 * 生成时间：01/11/2023 22:48:39
****************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using YixiaoAdmin.Models;
using YixiaoAdmin.IServices;
using YixiaoAdmin.IRepository;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;
using YixiaoAdmin.Common;

namespace YixiaoAdmin.Services
{

    public partial class  UserServices:BaseServices<User>, IUserServices
    {
        private IUserRepository _UserRepository;
        public UserServices(IUserRepository UserRepository)
        {
            this._UserRepository = UserRepository;
            base.baseRepository = _UserRepository;
        }
        public async Task<PagesResponse> QueryPages(QueryPageModel queryPageModel)
        {
             //自定义分页Response
            PagesResponse pagesResponse = new PagesResponse();
            //初始化查询表达式
            Expression<Func<User, bool>> whereExpression = PredicateBuilder.True<User>();
            //判断是否存在查询条件
            if (queryPageModel.Query != null)
            {
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

                        whereExpression = PredicateBuilder.And(whereExpression, (x) => x.Name .Contains( item.QueryStr));
                    }

                    else if (item.QueryField == "CreateTime")
                    {
                        whereExpression = PredicateBuilder.And(whereExpression, (x) => x.CreateTime.Date==Convert.ToDateTime(item.QueryStr.Trim()));
                    }

                    else if (item.QueryField == "Id")
                    {
                        whereExpression = PredicateBuilder.And(whereExpression, (x) => x.Id.Contains(item.QueryStr));
                    }

                }
            }
            pagesResponse.Success((await _UserRepository.Query(whereExpression, queryPageModel.Orderby, queryPageModel.CurrentPage, queryPageModel.PageNumber)).ToList());
            pagesResponse.count = (await _UserRepository.Query(whereExpression)).Count();
            return pagesResponse;
           
        }
    }
}
