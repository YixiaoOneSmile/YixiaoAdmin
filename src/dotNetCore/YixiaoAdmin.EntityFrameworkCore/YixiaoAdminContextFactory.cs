using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace YixiaoAdmin.EntityFrameworkCore
{
    public class YixiaoAdminContextFactory : IDesignTimeDbContextFactory<YixiaoAdminContext>
    {
        YixiaoAdminContext IDesignTimeDbContextFactory<YixiaoAdminContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<YixiaoAdminContext>();
            optionsBuilder.UseSqlServer(DbConfig.ConnectString);
            return new YixiaoAdminContext(optionsBuilder.Options);
        }
    }

    public class DbConfig
    {
       public  static readonly string ConnectString = "Server=[Your Database Server Adress];Database=YixiaoAdmin;User ID=[Your Database UserName];Password=[Your Database Password]";
    }
}