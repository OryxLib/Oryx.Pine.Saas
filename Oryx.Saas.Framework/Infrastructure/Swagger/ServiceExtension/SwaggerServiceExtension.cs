using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Oryx.Saas.Framework.Infrastructure.Swagger.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ryx.Saas.Framework.Infrastructure.Swagger.ServiceExtension
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Protected API", Version = "v1" });
                options.ResolveConflictingActions(apiDescriptionList =>
                {
                    //apiDescriptionList.
                    return apiDescriptionList.First();
                });
                //向生成的Swagger添加一个或多个“securityDefinitions”，用于API的登录校验
                //options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                //{
                //    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                //    In = ParameterLocation.Header,
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.OpenIdConnect,
                //    OpenIdConnectUrl = new Uri("http://localhost:5000/connect/authorize")
                //});

                //options.OperationFilter<AuthorizeCheckOperationFilter>(); // 添加IdentityServer4认证过滤
            });

            return services;
        }
    }
}
