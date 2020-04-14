using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Saas.Framework.Database.Redis.ServiceExtension
{
    public static class RedisServiceExtension
    {
        public static IServiceCollection AddRedis(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var redisHost = configuration["Database:Redis"];
            //aspnet distribute session  
            var redis = ConnectionMultiplexer.Connect(redisHost);
            services.AddDataProtection()
             .SetApplicationName("session_application_name")
             .PersistKeysToRedis(redis, "DataProtection-Keys");
            
            return services;
        }
    }
}
