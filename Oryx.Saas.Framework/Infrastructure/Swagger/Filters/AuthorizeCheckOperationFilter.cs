using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace Oryx.Saas.Framework.Infrastructure.Swagger.Filters
{
    class AuthorizeCheckOperationFilter : IOperationFilter
    {

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //获取是否添加登录特性
            var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
             .Union(context.MethodInfo.GetCustomAttributes(true))
             .OfType<AuthorizeAttribute>().Any();

            if (authAttributes)
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "暂无访问权限" });
                operation.Responses.Add("403", new OpenApiResponse { Description = "禁止访问" });
                //给api添加锁的标注
                var item = new OpenApiSecurityRequirement();
                var key = new OpenApiSecurityScheme();
                key.Name = "oauth2";
                key.Type = SecuritySchemeType.OpenIdConnect;
              
                item.Add(key, new[] { "JsPortalResource" });
                operation.Security = new List<OpenApiSecurityRequirement> { item };
            }
        }
    }
}
