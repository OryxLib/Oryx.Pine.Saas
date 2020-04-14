using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Oryx.Saas.Framework.Infrastructure.Serilogs.ServiceExtension
{
    public static class SerilogServiceExtension
    {
        public static IServiceCollection AddSerilogService(this IServiceCollection services)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs\\myapp.txt", Serilog.Events.LogEventLevel.Debug)
                .CreateLogger();
            services.AddSingleton<ILogger>(logger);

            return services;
        }
    }
}
