
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.WeChat;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Oryx.Pine.Saas.Portal.Infastructure.IdentityServer4.DbContext;
using Oryx.Pine.Saas.Portal.Infastructure.IdentityServer4.Models;
using Oryx.Saas.Framework.Business;
using Oryx.Saas.Framework.Business.Authencations.SaasAdminAccounts.AccountEntities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Pine.Saas.Portal.Infastructure.IdentityServer4.ServiceExtension
{
    public static class IdentityServer4ServicenExtension
    {
        public static IServiceCollection AddIdneityServer(this IServiceCollection services)
        {

            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();

            var aspIdUser = configuration["Infrastructure:IdentityServer4:sqlite:aspIdUser"];
            var is4Connection = configuration["Infrastructure:IdentityServer4:sqlite:Identity"];

            var authorityHost = configuration["Infrastructure:IdentityServer4:Authority"];

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes("secure"))
                , SecurityAlgorithms.HmacSha256);

            #region Identity Framework

            services.AddDbContext<SaasAuthenticationDbContext>(options =>
          options.UseSqlite(aspIdUser));

            //此处,因数据上下文的缘故,不进行用户数据隔离,只进行权限隔离
            services.AddIdentity<SaasAdminAccountEntity, IdentityRole>()
                //.AddClaimsPrincipalFactory<IUserClaimsPrincipalFactory<SaasAdminAccountEntity>>()
                .AddEntityFrameworkStores<SaasAuthenticationDbContext>()
                .AddDefaultTokenProviders();


            //可以作为IdentityServer 进行认证
            services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
               .AddDeveloperSigningCredential()
               //.AddSigningCredential(signingCredentials)
               //.AddInMemoryIdentityResources(Config.Ids)
               //.AddInMemoryApiResources(Config.Apis)
               //.AddInMemoryClients(Config.Clients)
               //.AddTestUsers(TestUsers.Users)
               .AddAspNetIdentity<SaasAdminAccountEntity>()
               .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlite(is4Connection,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlite(is4Connection,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
               ;

            #endregion

            /*The EF DbContext Init in Middleware*/

            //
            //设置web端的认证资源 ,资源名 
            //后续将资源及认证分离
            //配置web端访问接口
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = authorityHost;
                    options.RequireHttpsMetadata = false;
                    options.Audience = "JsPortalResource";
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = ctx =>
                      {
                          var accessToken = ctx.Request.Query["access_token"];
                          if (!string.IsNullOrEmpty(accessToken))
                          {
                              ctx.Token = accessToken;
                          }
                          return Task.CompletedTask;
                      }
                    };
                });

            //设置mvc的认证资源, 资源名 
            //可以作为mvc client访问自己
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("Cookies")
                //.AddWeChat(options =>
                //{
                //    options.AppId = "";
                //    options.AppSecret = "";
                //    options.UseCachedStateDataFormat = true;
                //    options.SaveTokens = true;
                //})
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = authorityHost;
                    options.RequireHttpsMetadata = false;

                    options.ClientId = "mvc";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code";
                    options.UsePkce = true;

                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;


                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("JsPortalResource");
                    options.Scope.Add("offline_access");

                    options.ClaimActions.MapJsonKey("website", "website");
                });
            return services;
        }
    }
}
