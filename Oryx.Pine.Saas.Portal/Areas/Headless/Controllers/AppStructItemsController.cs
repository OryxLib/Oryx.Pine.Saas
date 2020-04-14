using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oryx.Saas.Framework.Applications.Controllers;
using Oryx.Saas.Framework.Business;
using Oryx.Saas.Framework.Business.ServiceSection.SaasServiceSection.ServiceEntities;

namespace Oryx.Pine.Saas.Portal.Areas.Heaadless.Controllers
{
    [Area("Headless")]
    public class AppStructItemsController : BaseBackendController<AppStructItem>
    {
        public AppStructItemsController(SaasServiceDbContext _dbContext)
            : base(_dbContext)
        {
        }
    }
}