using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using YixiaoAdmin.EntityFrameworkCore;
using YixiaoIdentity.Managment.Auth;

namespace YixiaoAdmin.WebApi
{
    public class Startup
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">注入的配置文件</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; } //配置文件
        public string ApiName { get; set; } = "YixaoAdminApi";  //Swagger显示的Api名称

        /// <summary>
        /// 添加服务到容器中
        /// </summary>
        /// <param name="services">服务</param>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;//����ѭ������
                    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();//json�ַ�����Сдԭ�����
                });

            #region EFCore
           //注册当前应用的数据库DbContext
            services.AddDbContext<YixiaoAdminContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("YixiaoAdminDatabase")));
            #endregion

            #region Swagger

           //获取当前目录
            var basePath = ApplicationEnvironment.ApplicationBasePath;

         
            // 添加Swagger服务
            services.AddSwaggerGen(c =>
            {
                // 添加自定义的认证策略
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    // API的版本
                    Version = "V1",
                    // API的标题，这里使用了ApiName变量
                    Title = $"{ApiName} 接口文档——dotNetcore 6.0", //标题
                    // API的描述
                    Description = $"{ApiName} HTTP API V1", //描述
                    // API的联系方式
                    Contact = new OpenApiContact { Name = ApiName, Email = "792554917@qq.com", Url = new Uri("http://www.i5g.club") }, //联系方式
                    // API的许可证信息
                    License = new OpenApiLicense { Name = ApiName, Url = new Uri("http://www.i5g.club") } //许可证
                });

                // 对API的操作进行排序，这里是按照相对路径进行排序
                c.OrderActionsBy(o => o.RelativePath);

                // 获取API的XML注释文件路径
                var xmlPath = Path.Combine(basePath, "YixiaoAdmin.WebApi.xml");

                // 获取模型的XML注释文件路径
                var xmlModelPath = Path.Combine(basePath, "YixiaoAdmin.Models.xml");

                // 将API的XML注释添加到Swagger
                c.IncludeXmlComments(xmlPath, true);

                // 将模型的XML注释添加到Swagger
                c.IncludeXmlComments(xmlModelPath);

                // 添加IdentityServer4的操作过滤器
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                // 添加OAuth2的安全定义，这部分代码被注释掉了
                // c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                // {
                //     Type = SecuritySchemeType.OAuth2,
                //     Flows = new OpenApiOAuthFlows
                //     {
                //         Implicit = new OpenApiOAuthFlow
                //         {
                //             AuthorizationUrl = new Uri("http://localhost:5000/connect/authorize"),
                //             Scopes = new Dictionary<string, string>
                //             {
                //                 { "api1", "api1" },
                //             }
                //         }
                //     }
                // });

                // 添加名为"oauth2"的安全定义，类型为ApiKey，位置在Header
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

            });

            #endregion

            #region IdentityServer4

            // 自定义的认证策略，这里是使用Bearer Token进行认证
            //services.AddAuthentication("Bearer")
            //.AddJwtBearer("Bearer", options =>
            //{
            //    options.Authority = "http://localhost:5000"; // 认证服务器的地址
            //    options.RequireHttpsMetadata = false; // 是否需要Https
            //    options.Audience = "api1"; // 该服务的名称，需要和认证服务器上的配置一致
            //});

            // 添加授权策略
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("UserDefined", builder =>
            //    {
            //        builder.AddRequirements(new UserDefinedRequirement()); // 添加自定义的授权要求
            //    });
            //});

            // 注册IAuthorizationHandler的实现，IAuthorizationHandler是授权处理器，用于处理自定义的授权要求
            //services.AddSingleton<IAuthorizationHandler, UserDefinedHandler>();
            
            #endregion

            #region Authentication

            #region Jwt
            //读取配置文件
            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsBase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            #endregion
      
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })// AddAuthentication("Bearer")
             .AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = signingKey,
                     ValidateIssuer = true,
                     ValidIssuer = audienceConfig["Issuer"],
                     ValidateAudience = true,
                     ValidAudience = audienceConfig["Audience"],
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero,
                     RequireExpirationTime = true,
                 };

             });
            #endregion


        }

        /// <summary>
        /// 配置HTTP请求管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //判断是否处于开发环境
            if (env.IsDevelopment())
            {
                //开发环境显示异常
                app.UseDeveloperExceptionPage();
            }

            #region Swagger

           //启用Swagger
            app.UseSwagger();

           //启用Swagger用户界面
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/V1/swagger.json", $"{ApiName} V1");

                //设置路由前缀，如果为空，表示直接在根目录下访问，注意localhost:8001/swagger是无法访问的，去launchSettings.json的launchUrl去修改，如果不想有前缀直接写成c.RoutePrefix = "doc";
                c.RoutePrefix = "";
            });

            #endregion


            #region Cors
            app.UseCors(builder => builder.WithOrigins("http://localhost:8080")

            .AllowAnyHeader()

            .AllowAnyMethod());
            #endregion

            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #region Autofac
        /// <summary>
        /// AutoFac 注册服务
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;//获取项目路径
            var servicesDllFile = Path.Combine(basePath, "YixiaoAdmin.Services.dll");
            var repositoryDllFile = Path.Combine(basePath, "YixiaoAdmin.Repository.dll");

            if (!(File.Exists(servicesDllFile) && File.Exists(repositoryDllFile)))
            {
                throw new Exception("Repository.dll or Services.dll is not exists!");
            }
            // 批量注入 Services.dll
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            builder.RegisterAssemblyTypes(assemblysServices)
                      .AsImplementedInterfaces()
                      .InstancePerDependency()
                      .EnableInterfaceInterceptors();//引用Autofac.Extras.DynamicProxy;


            // 批量注入 Repository.dll
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository)
                   .AsImplementedInterfaces()
                   .InstancePerDependency();

        }
        #endregion

    }
}
