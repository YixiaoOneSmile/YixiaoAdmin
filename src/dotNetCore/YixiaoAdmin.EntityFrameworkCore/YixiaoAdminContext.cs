using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using YixiaoAdmin.Models;

namespace YixiaoAdmin.EntityFrameworkCore
{
    public class YixiaoAdminContext : DbContext
    {
        public YixiaoAdminContext(DbContextOptions<YixiaoAdminContext> options) : base(options)
        {
        }

        /// <summary>
        /// 将数据实体填写到此处
        /// </summary>
        //public DbSet<Student> Student { get; set; }


        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<User> User { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<Role> Role { get; set; }
        /// <summary>
        /// 功能
        /// </summary>
        public DbSet<Right> Right { get; set; }
        /// <summary>
        /// 功能角色
        /// </summary>
        public DbSet<RoleRight> RoleRight { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            //配置级联删除的样例
            //modelBuilder.Entity<PalletIdentificationCard>().HasMany(b => b.PalletIdentificationCardSubOrder).WithOne(p => p.PalletIdentificationCard)
            //    .HasForeignKey(p => p.PalletIdentificationCardId)
            //    .OnDelete(DeleteBehavior.Cascade);
            //base.OnModelCreating(modelBuilder);


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new EFLoggerProvider());
            optionsBuilder.UseLoggerFactory(loggerFactory);
            //如需查看EFCore日志，取消该注释即可
            //base.OnConfiguring(optionsBuilder);
        }

    }
    public class EFLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName) => new EFLogger(categoryName);
        public void Dispose() { }
    }

    public class EFLogger : ILogger
    {
        private readonly string categoryName;

        public EFLogger(string categoryName) => this.categoryName = categoryName;

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            var logContent = formatter(state, exception);
            Console.WriteLine();
            Console.WriteLine(logContent);
        }
    }



}
