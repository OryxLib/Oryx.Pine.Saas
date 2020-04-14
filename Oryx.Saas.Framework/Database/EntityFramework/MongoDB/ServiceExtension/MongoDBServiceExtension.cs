using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Oryx.Saas.Framework.Database.EntityFramework.MongoDB.ServiceExtension
{
    public static class MongoDBServiceExtension
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services)
        { 
            services.AddTransient<MongoClient>(provider =>
            {
                //var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
                var configuration = provider.GetService<IConfiguration>();
                var mongoHost = configuration["Database:MongoDb"];
                var client = new MongoClient(mongoHost);
                return client;
            });
            return services;
        }
    }
}
