using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YixiaoAdmin.IServices;
using YixiaoAdmin.Models;
using YixiaoAdmin.Common;

//这是 RoleRight 控制器

namespace YixiaoAdmin.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoleRightController : ControllerBase
    {
        private readonly IRoleRightServices _RoleRightServices;


        public RoleRightController(IRoleRightServices RoleRightServices)
        {
            _RoleRightServices = RoleRightServices ?? 
                                       throw new ArgumentNullException(nameof(RoleRightServices));
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IList<RoleRight>> All()
        {
            return await _RoleRightServices.Query();
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="queryPageModel">查询模型</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<IEnumerable<RoleRight>>> Pages(QueryPageModel queryPageModel)
        {
            return Ok(await _RoleRightServices.QueryPages(queryPageModel));
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<RoleRight> Get(string Id)
        {
            return await _RoleRightServices.QueryById(Id);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Post(RoleRight viewModel)
        {

            return await _RoleRightServices.Add(viewModel);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> Put(RoleRight viewModel)
        {
            return await _RoleRightServices.Update(viewModel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> Delete(string Id)
        {
            return await _RoleRightServices.RemoveById(Id);
        }
    }
}
