using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oryx.Saas.Framework.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Saas.Framework.Database.EntityFramework.Sqlite.ServiceExtension
{
    public static class SqliteServiceExtension
    {
        public static IServiceCollection AddSqliteService(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var mssqlHost = configuration["Database:Sqlite"];
            services.AddDbContextPool<SaasServiceDbContext>(optBuilder =>
            {
                optBuilder.UseSqlite(mssqlHost, opts =>
                {
                    opts.CommandTimeout(30);   
                });
            }, 10);

            return services;
        }
    }
}
