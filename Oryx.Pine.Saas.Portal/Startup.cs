using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Oryx.Pine.Saas.Portal.Infastructure.IdentityServer4.Middleware;
using Oryx.Pine.Saas.Portal.Infastructure.IdentityServer4.ServiceExtension;
using Oryx.Saas.Framework.Startup;
using Oryx.Saas.Framework.Applications.WebSockets.Extension.Builder;
using Oryx.Pine.Saas.Portal.Handlers;
using Microsoft.AspNetCore.StaticFiles;

namespace Oryx.Pine.Saas.Portal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration; 
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSaasFramework();
            services.AddIdneityServer();
            services.AddTransient<WebSocketHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider service)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".md"] = "text/html";
            app.UseStaticFiles(new StaticFileOptions()
            {
                ContentTypeProvider = provider
            });

            app.UseRouting();
            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());


            app.UserIS4();
            app.UseSaasFramework();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UserOryxWebSocket(options =>
            {
                options.OptionsDic.Add("/websocket", service.GetService<WebSocketHandler>());
            });

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "areas",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                  );
                endpoints.MapAreaControllerRoute(
                    name: "areas",
                    areaName: "Headless",
                    pattern: "Headless/{controller=Home}/{action=Index}/{id?}"
                  );
                endpoints.MapAreaControllerRoute(
                    name: "areas",
                    areaName: "Identity",
                    pattern: "Identity/{controller=Home}/{action=Index}/{id?}"
                 );
                endpoints.MapAreaControllerRoute(
                    name: "areas",
                    areaName: "DataAPi",
                    pattern: "DataAPi/{controller=Home}/{action=Index}/{id?}"
                 );
            });
        }
    }
}
