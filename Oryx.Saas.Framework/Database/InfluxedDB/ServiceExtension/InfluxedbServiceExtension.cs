using InfluxDB.Collector;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Saas.Framework.Database.InfluxedDB.ServiceExtension
{
    public static class MongoDBServiceExtension
    {
        public static IServiceCollection AddInfluxdb(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var indbHost = configuration["Database:InfluxdDB"];
            Metrics.Collector = new CollectorConfiguration() 
                .Tag.With("host", Environment.GetEnvironmentVariable("COMPUTERNAME"))
                .Batch.AtInterval(TimeSpan.FromSeconds(2))
                .WriteTo.InfluxDB(indbHost, "data")
                .CreateCollector();

            services.AddSingleton(Metrics.Collector);
            return services;
        }
    }
}
