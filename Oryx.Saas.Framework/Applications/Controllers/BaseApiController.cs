using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oryx.Saas.Framework.Business.Entities;
using Oryx.Saas.Framework.Model;
using Oryx.Saas.Framework.ViewModel;
using System;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Oryx.Saas.Framework.Applications.Controllers
{
    public class BaseApiController<T> : BaseController
             where T : SaasBaseEntity, new()
    {
        DbContext dbContext;
        ModelMapper modelMapper;
        public BaseApiController(DbContext _dbContext)
            : base(_dbContext)
        {
            dbContext = _dbContext;
            modelMapper = new ModelMapper(dbContext);
        }

        public async Task<IActionResult> Index(int page = 1, int size = 15)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await modelMapper.ModelToTable<T>();
            });
            return Json(apiMsg);
        }

        public async Task<IActionResult> List(string query, int page = 1, int limit = 15)
        {
            ApiMessage apiMsg;
            var code = 0;
            var count = 0;
            object data = null;
            var msg = "";
            try
            {
                var sqlQuery = dbContext.Set<T>().AsQueryable();
                if (!string.IsNullOrEmpty(query))
                {
                    sqlQuery = sqlQuery.Where(query);
                }

                count = await sqlQuery.CountAsync();
                data = await sqlQuery.OrderBy("CreateTime").Skip((page - 1) * limit).Take(limit).ToDynamicListAsync();
            }
            catch (Exception exc)
            {
                msg = exc.Message;
            }

            return Json(new { code, count, data, msg });
        }

        public async Task<IActionResult> AddOrUpdate(Guid? Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                if (Id != null)
                {
                    var data = await dbContext.Set<T>().FirstAsync(x => x.Id == Id.Value);
                    return await modelMapper.ModelToData(data);
                }
                return null;
            });
            return Json(apiMsg);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(T userInfoModel)
        {
            ViewData["ModelType"] = modelMapper.ModelToFormControl<T>();
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                if (Guid.Empty == userInfoModel.Id)
                {
                    userInfoModel.Id = Guid.NewGuid();
                    await dbContext.AddAsync(userInfoModel);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    dbContext.Update(userInfoModel);
                    await dbContext.SaveChangesAsync();
                }
            });

            return Json(apiMsg);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var entity = await dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == Id);
                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();
            });

            return Json(apiMsg);
        }
    }
}
