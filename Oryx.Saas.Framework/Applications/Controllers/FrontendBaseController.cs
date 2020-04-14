using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Saas.Framework.Applications.Controllers
{
    public class FrontendBaseController : BaseController
    {
        DbContext dbContext;
        public FrontendBaseController(DbContext _dbContext)
            : base(_dbContext)
        {
            dbContext = _dbContext;
        }
    }
}
