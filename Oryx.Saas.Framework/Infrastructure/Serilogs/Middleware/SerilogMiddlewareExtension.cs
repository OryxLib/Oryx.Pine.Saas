using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Saas.Framework.Infrastructure.Serilogs.Middleware
{
    public static class SerilogMiddlewareExtension
    {
        public static IApplicationBuilder UserSerilogServer(this IApplicationBuilder applicationBuilder)
        {  
            return applicationBuilder;
        }
    }
}
