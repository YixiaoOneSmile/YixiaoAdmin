using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YixiaoAdmin.IServices;
using YixiaoAdmin.Models;

namespace YixiaoAdmin.WebApi.AuthHelper
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserServices _UserServices;


        public AuthController(IUserServices UserServices)
        {
            _UserServices = UserServices ??
                                       throw new ArgumentNullException(nameof(UserServices));
        }
        [HttpGet]
        public async Task<IActionResult> GetJwtStr(string name, string pass)
        {
            User user = await GetUserByCredentials(name, pass);
            if (user == null)
            {
                return NotFound();
            }
            var jwtStr = JwtHelper.IssueJwt(GetTokenModelJwt(user));
            return Ok(new
            {
                success = true,
                role = user.Role,
                token = jwtStr
            });
        }

        private TokenModelJwt GetTokenModelJwt(User user)
        {
            return new TokenModelJwt
            {
                Uid = user.Id,
                Role = user.Role.Name
            };
        }

        private async Task<User> GetUserByCredentials(string name, string pass)
        {
            return (await _UserServices.Query(x => x.UserName == name && x.Password == pass))
                .Include(x => x.Role)
                .ThenInclude(x => x.RoleRights)
                .ThenInclude(x => x.Right)
                .FirstOrDefault();
        }
    }
}
