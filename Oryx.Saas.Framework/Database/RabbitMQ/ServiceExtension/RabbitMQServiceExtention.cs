
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Saas.Framework.Database.RabbitMQ.ServiceExtension
{
    public static class RabbitMQServiceExtention
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
        {
            services.AddSingleton<RabbitMQClient>();
            return services;
        }
    }
}
