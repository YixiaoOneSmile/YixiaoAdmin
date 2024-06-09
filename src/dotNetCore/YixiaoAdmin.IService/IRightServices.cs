/****************************************************
 * 本文件由T4模板生成，请将本文件复制到YixiaoAdmin.IServices类库中使用
 * 文件名：IRightServices.cs
 * 生成时间：12/22/2020 21:29:34
****************************************************/

using YixiaoAdmin.Models;
using YixiaoAdmin.Common;
using YixiaoAdmin.Models.ViewModels;
using System.Threading.Tasks;

namespace YixiaoAdmin.IServices
{

    public partial interface  IRightServices:IBaseServices<Right>
    {
        /// <summary>
        /// 分页查询拓展
        /// </summary>
        /// <param name="queryPageModel">分页搜索模型</param>
        /// <returns></returns>
        Task<PagesResponse> QueryPagesExpand(QueryPageModel queryPageModel);
    }
}
