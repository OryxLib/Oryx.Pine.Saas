using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Oryx.Saas.Framework.Business.Authencations.SaasAdminAccounts.AccountEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.Pine.Saas.Portal.Infastructure.IdentityServer4.DbContext
{
    public class SaasAuthenticationDbContext : IdentityDbContext<SaasAdminAccountEntity>
    {
        public SaasAuthenticationDbContext(DbContextOptions<SaasAuthenticationDbContext> options)
            : base(options)
        {
        }
    }
}
