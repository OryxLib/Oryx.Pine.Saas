using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Oryx.Saas.Framework.Applications.Controllers.MongoDb;
using Oryx.Saas.Framework.Business;

namespace Oryx.Pine.Saas.Portal.Areas.Heaadless.Controllers
{
    [Area("Headless")]
    public class AppStructDataController : BaseMongoController
    {
        public AppStructDataController(MongoClient client, SaasServiceDbContext _dbContext) 
            : base(client, _dbContext)
        {
        }
    }
}