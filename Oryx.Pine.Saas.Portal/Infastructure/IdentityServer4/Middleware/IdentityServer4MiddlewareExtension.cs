using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Oryx.Pine.Saas.Portal.Infastructure.IdentityServer4.DbContext;
using Oryx.Pine.Saas.Portal.Infastructure.IdentityServer4.Models;
using Oryx.Saas.Framework.Business.Authencations.SaasAdminAccounts.AccountEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.Pine.Saas.Portal.Infastructure.IdentityServer4.Middleware
{
    public static class IdentityServer4MiddlewareExtension
    {
        public static IApplicationBuilder UserIS4(this IApplicationBuilder app)
        {
            app.InitializeIS4Database();
            app.UseIdentityServer();
            return app;
        }

        public static IApplicationBuilder InitializeIS4Database(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                //serviceScope.ServiceProvider.GetRequiredService<SaasAuthenticationDbContext>(); ;
                var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<SaasAdminAccountEntity>>();

                if (!userMgr.Users.Any())
                {
                    userMgr.CreateAsync(new SaasAdminAccountEntity
                    {
                        UserName = "bob",
                        Email = "bob@admin.com"
                    }, "bob").Wait();
                } 

                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.Clients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.Ids)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.Apis)
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }

            return app;
        }
    }
}
