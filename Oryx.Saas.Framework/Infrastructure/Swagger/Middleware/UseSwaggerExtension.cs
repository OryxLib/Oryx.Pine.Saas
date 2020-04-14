using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Saas.Framework.Infrastructure.Swagger.Middleware
{
    public static class UseSwaggerExtension
    {
        public static IApplicationBuilder UseSwaggerService(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "智能服务端API");
                //options.OAuthClientId("JsPortalResource");//客服端名称
                //options.OAuthAppName("Demo API - 知页智能服务端演示"); // 描述
            });

            return applicationBuilder;
        }
    }
}
