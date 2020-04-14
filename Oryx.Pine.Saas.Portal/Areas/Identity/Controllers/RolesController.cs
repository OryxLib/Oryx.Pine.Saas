using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oryx.Pine.Saas.Portal.Infastructure.IdentityServer4.DbContext;
using Oryx.Saas.Framework.Applications.Controllers.Identity;

namespace Oryx.Pine.Saas.Portal.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class RolesController : BaseIdentityRolesBaseController<IdentityRole>
    {
        public RolesController(SaasAuthenticationDbContext _dbContext)
            : base(_dbContext)
        {
        }
    }
}