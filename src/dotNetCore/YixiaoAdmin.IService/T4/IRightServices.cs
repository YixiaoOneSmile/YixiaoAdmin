/****************************************************
 * 本文件由T4模板生成，重新生成T4模板后会导致代码丢失
 * 如需修改请使用partial关键词
 * 文件名：IRightServices.cs
 * 生成时间：01/11/2023 22:48:11
****************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YixiaoAdmin.Common;
using YixiaoAdmin.Models;

namespace YixiaoAdmin.IServices
{

    public partial interface  IRightServices:IBaseServices<Right>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="name">搜索表达式</param>
        /// <param name="order">排序表达式</param>
        /// <param name="desc">是否开启倒序，true为开启倒序</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageNumber">每页显示条数</param>
        /// <returns></returns>
        Task<PagesResponse> QueryPages(QueryPageModel queryPageModel);

        
    }
}
