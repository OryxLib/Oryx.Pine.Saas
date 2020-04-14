using Microsoft.AspNetCore.Builder;
using Oryx.Saas.Framework.Applications.WebSockets.Extension.Builder;
using Oryx.Saas.Framework.Infrastructure.Swagger.Middleware;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Saas.Framework.Startup
{
    public static class MiddlewarePrepareExtension
    {
        public static IApplicationBuilder UseSaasFramework(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSwaggerService();
              
            return applicationBuilder;
        }
    }
}
