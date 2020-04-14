using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oryx.Saas.Framework.Business;

namespace Oryx.Saas.Framework.Database.EntityFramework.Mysql.ServiceExtension
{
    public static class MysqlServiceExtension
    {
        public static IServiceCollection AddMysqlDB(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var mysqlHost = configuration["Database:MysqlDB"];
            services.AddDbContextPool<SaasServiceDbContext>(optBuilder =>
            {
                optBuilder.UseMySql(mysqlHost, opts =>
                {
                    opts.CommandTimeout(60);
                    opts.EnableRetryOnFailure(3);
                    opts.MaxBatchSize(1000);
                    //opts.MigrationsAssembly("Oryx.Content.Portal");
                });
            }, 10);

            return services;
        }
    }
}
