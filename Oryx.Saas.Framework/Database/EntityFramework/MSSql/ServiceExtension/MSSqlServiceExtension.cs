using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oryx.Saas.Framework.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Saas.Framework.Database.EntityFramework.MSSql.ServiceExtension
{
    public static class MSSqlServiceExtension
    {
        public static IServiceCollection AddMSsqlDB(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var mssqlHost = configuration["Database:MssqlDB"];
            services.AddDbContextPool<SaasServiceDbContext>(optBuilder =>
            {
                optBuilder.UseSqlServer(mssqlHost, opts =>
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
