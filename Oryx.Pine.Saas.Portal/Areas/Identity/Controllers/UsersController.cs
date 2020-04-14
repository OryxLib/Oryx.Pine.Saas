using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oryx.Pine.Saas.Portal.Infastructure.IdentityServer4.DbContext;
using Oryx.Saas.Framework.Applications.Controllers;
using Oryx.Saas.Framework.Applications.Controllers.Identity;
using Oryx.Saas.Framework.Business.Authencations.SaasAdminAccounts.AccountEntities;

namespace Oryx.Pine.Saas.Portal.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class UsersController : BaseIdentityUserBaseController<SaasAdminAccountEntity>
    {
        SaasAuthenticationDbContext dbContext;
        public UsersController(SaasAuthenticationDbContext _dbContext)
             : base(_dbContext)
        {
            dbContext = _dbContext;
        }
    }
}