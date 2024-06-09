using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace YixiaoIdentity.Managment.Auth
{
    //public class UserDefinedHandler : AuthorizationHandler<UserDefinedRequirement>
    //{
    //    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserDefinedRequirement requirement)
    //    {
    //        //注意！请在此处配置该策略所需要获取权限的数据库的连接字符串
    //        string connectionString = @"Server=www.i5g.club;Database=YixiaoAdmin.Identity.Managment;User ID=sa;Password=asqmkj147@";

    //        //验证成功标识
    //        bool IsAuthorizeSuccess = false;

    //        //获取被访问的受保护Action与其Controller的名称
    //        RouteEndpoint route = context.Resource as RouteEndpoint;
    //        route.RoutePattern.Defaults.TryGetValue("action", out object actionName);
    //        route.RoutePattern.Defaults.TryGetValue("controller", out object controllerName);

    //        //获取用户角色列表
    //        var roleList = context.User.Claims.Where(x => x.Type == JwtClaimTypes.Role).ToList();
            
    //        //构造数据库访问所需的optionBuilder
    //        var optionsBuilder = new DbContextOptionsBuilder<AuthDbContext>();
    //        optionsBuilder.UseSqlServer(connectionString);

    //        //构造Context
    //        using (var dbContext = new AuthDbContext(optionsBuilder.Options))
    //        {
    //            //遍历用户所有角色查看是否有满足条件的角色
    //            foreach (var role in roleList)
    //            {
    //                //取出Claim的值
    //                string roleName = role?.Value;

    //                //查找是否存在能够访问该控制器与Action的角色
    //                var user = dbContext.Sys_Roles.Where(x => x.Name == roleName).Select(s=>new
    //                    {
    //                        Controller = s.Controllers.Where(x => x.Name == controllerName.ToString()).Select(x => new
    //                        {
    //                            Actions = x.Actions.FirstOrDefault(x=>x.Name== actionName.ToString())
    //                        })
    //                    }).ToList();
    //                if (user.First().Controller == null)
    //                {
    //                    continue;
    //                }

    //                if (user.First().Controller.First().Actions != null)
    //                {
    //                    IsAuthorizeSuccess = true;
    //                    break;
    //                }
    //            }
    //            //如果查找失败则阻断http请求并返回没有权限
    //            if (IsAuthorizeSuccess == false)
    //            {
    //                context.Fail();
    //                return Task.CompletedTask;
    //            }
    //        }
    //        //TODO 未来会加一个Log
    //        Console.WriteLine("Controller：{0} ,Action :{1} is Authorized", controllerName, actionName);

    //        //如果查找成共则继续http请求
    //        context.Succeed(requirement);
    //        return Task.CompletedTask;
    //    }
    //}
}
