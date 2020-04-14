using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Oryx.Saas.Framework.Business.Authencations.SaasAdminAccounts.AccountEntities;
using Oryx.Saas.Framework.Business.Authencations.TopAdminAccounts.AccountEntities;
using Oryx.Saas.Framework.Business.ServiceSection.SaasServiceSection.ServiceEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Saas.Framework.Business
{
    public class SaasServiceDbContext : DbContext
    {
        public SaasServiceDbContext(DbContextOptions<SaasServiceDbContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }



        #region Top Service

        //public DbSet<TopAdminAccountEntity> TopAdmin { get; set; }

        #endregion

        #region Saas Service

        //public DbSet<SaasAdminAccountEntity> SaasAdmin { get; set; }
        public DbSet<AppHeader> AppHeader { get; set; }
        public DbSet<AppStruct> AppStructs { get; set; }
        public DbSet<AppStructItem> AppStructItem { get; set; }
        public DbSet<SaasAdminConsume> SaasAdminConsume { get; set; }
        public DbSet<SaasAdminWallet> SaasAdminWallet { get; set; }


        #endregion

    }
}
