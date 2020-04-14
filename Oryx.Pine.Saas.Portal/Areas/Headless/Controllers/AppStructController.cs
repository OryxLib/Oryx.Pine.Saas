using Microsoft.AspNetCore.Mvc;
using Oryx.Saas.Framework.Applications.Controllers;
using Oryx.Saas.Framework.Business;
using Oryx.Saas.Framework.Business.ServiceSection.SaasServiceSection.ServiceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.Pine.Saas.Portal.Areas.Heaadless.Controllers
{
    [Area("Headless")]
    public class AppStructController : BaseBackendController<AppStruct>
    {
        public AppStructController(SaasServiceDbContext _dbContext)
            : base(_dbContext)
        {
        }

    }
}
