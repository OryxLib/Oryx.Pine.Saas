using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oryx.Saas.Framework.Applications.WebSockets.Extension.DependencyInjection;
using Oryx.Saas.Framework.Database.EntityFramework.MongoDB.ServiceExtension;
using Oryx.Saas.Framework.Database.EntityFramework.MSSql.ServiceExtension;
using Oryx.Saas.Framework.Database.EntityFramework.Mysql.ServiceExtension;
using Oryx.Saas.Framework.Database.EntityFramework.Sqlite.ServiceExtension;
using Oryx.Saas.Framework.Database.InfluxedDB.ServiceExtension;
using Oryx.Saas.Framework.Database.RabbitMQ.ServiceExtension;
using Oryx.Saas.Framework.Database.Redis.ServiceExtension;
using Oryx.Saas.Framework.Helpers.ConfigureOptions;
using Oryx.Saas.Framework.Infrastructure.Serilogs.ServiceExtension;
using ryx.Saas.Framework.Infrastructure.Swagger.ServiceExtension;

namespace Oryx.Saas.Framework.Startup
{
    public static class ServicePrepareExtension
    {
        public static IServiceCollection AddSaasFramework(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            //services.ConfigureOptions<DatabaseOptions>()
            //    .ConfigureOptions<IdentityServerOptions>()
            //    .ConfigureOptions<InfrastructureOptions>();

            var dboptions = configuration.GetSection("Database").Get<DatabaseOptions>();
            var isoptions = configuration.GetSection("Infrastructure:IdentityServer4").Get<IdentityServerOptions>();
            var ifoptions = configuration.GetSection("Infrastructure").Get<InfrastructureOptions>();

            #region Database
            //默认添加mongodb
            services.AddMongoDb();

            //添加關係数据库
            switch (dboptions.DefaultRSDB)
            {
                case "MssqlDB":
                    services.AddMSsqlDB();
                    break;
                case "Sqlite":
                    services.AddSqliteService();
                    break;
                case "MysqlDB":
                    services.AddMysqlDB();
                    break;
            }

            if (dboptions.InfluxdDB.Eanbled)
            {
                services.AddInfluxdb();
            }

            if (dboptions.RabbitMQ.Eanbled)
            {
                services.AddRabbitMQ();
            }

            if (dboptions.Redis.Eanbled)
            {
                services.AddRedis();
            }

            #endregion

            //日志
            services.AddSerilogService();

            services.AddSwaggerService();

            services.AddOryxWebSocket();

            return services;
        }
    }
}
