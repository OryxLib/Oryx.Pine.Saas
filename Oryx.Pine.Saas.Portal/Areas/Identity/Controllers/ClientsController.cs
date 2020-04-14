using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oryx.Saas.Framework.Applications.Controllers.Identity;

namespace Oryx.Pine.Saas.Portal.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class ClientsController : BaseIdentityClientsBaseController<Client>
    {
        public ClientsController(ConfigurationDbContext _dbContext) : base(_dbContext)
        {
        }
         
    }
}