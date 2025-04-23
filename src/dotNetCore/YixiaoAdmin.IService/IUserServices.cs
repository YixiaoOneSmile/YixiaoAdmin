/****************************************************
 * 本文件由T4模板生成，请将本文件复制到YixiaoAdmin.IServices类库中使用
 * 文件名：IUserServices.cs
****************************************************/

using YixiaoAdmin.Models;
using YixiaoAdmin.Common;
using YixiaoAdmin.Models.ViewModels;
using System.Threading.Tasks;

namespace YixiaoAdmin.IServices
{

    public partial interface  IUserServices:IBaseServices<User>
    {
        /// <summary>
        /// 分页查询拓展
        /// </summary>
        /// <param name="queryPageModel">分页搜索模型</param>
        /// <returns></returns>
        Task<PagesResponse> QueryPagesExpand(QueryPageModel queryPageModel);
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        Task<Response> Login(string Username, string Password);
    }
}
