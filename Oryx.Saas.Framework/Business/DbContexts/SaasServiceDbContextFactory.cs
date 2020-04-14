using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Saas.Framework.Business
{
    public class SaasServiceDbContextFactory : IDesignTimeDbContextFactory<SaasServiceDbContext>
    {
        public const string UseDb = "Sqlite";

        public SaasServiceDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", false, true);
            var Configuration = builder.Build();


            var optionsBuilder = new DbContextOptionsBuilder<SaasServiceDbContext>();
            optionsBuilder.UseSqlite("Data Source=blog.db");
            return new SaasServiceDbContext(optionsBuilder.Options);
        }
    }
}
