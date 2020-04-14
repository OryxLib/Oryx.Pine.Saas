﻿using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oryx.Saas.Framework.Model;
using Oryx.Saas.Framework.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Saas.Framework.Applications.Controllers.Identity
{
    public class BaseIdentityPersistedGrantsBaseController<T> : Controller
            where T : PersistedGrant, new()
    {
        DbContext dbContext;
        ModelMapper modelMapper;
        public BaseIdentityPersistedGrantsBaseController(DbContext _dbContext)
        {
            dbContext = _dbContext;
            modelMapper = new ModelMapper(dbContext);
        }

        public async Task<IActionResult> Index(int page = 1, int size = 15)
        {
            ViewData["ModelTable"] = await modelMapper.ModelToTable<T>();

            return View();
        }

        public async Task<IActionResult> List(string query, int page = 1, int limit = 15)
        {
            ApiMessage apiMsg;
            var code = 0;
            var count = 0;
            object data = null;
            var msg = "";
            //            code: 0
            //count: 1000
            //data: [,…]
            //msg: ""
            try
            {
                var sqlQuery = dbContext.Set<T>().AsQueryable();

                if (!string.IsNullOrEmpty(query))
                {
                    sqlQuery = sqlQuery.Where(query);
                }

                count = await sqlQuery.CountAsync();
                data = await sqlQuery.OrderBy("Id").Skip((page - 1) * limit).Take(limit).ToDynamicListAsync();
            }
            catch (Exception exc)
            {
                msg = exc.Message;
            }

            return Json(new { code, count, data, msg });
        }

        public async Task<IActionResult> AddOrUpdate(string Id)
        {
            ViewData["ModelType"] = await modelMapper.ModelToFormControl<T>();

            if (Id != null)
            {
                var data = await dbContext.Set<T>().FirstAsync(x => x.Key == Id);
                ViewData["ModelData"] = await modelMapper.ModelToData(data);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(T userInfoModel)
        {
            ViewData["ModelType"] = modelMapper.ModelToFormControl<T>();
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                if (string.IsNullOrEmpty(userInfoModel.Key))
                {
                    userInfoModel.Key = Guid.NewGuid().ToString();
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
        public async Task<IActionResult> Delete(string Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var entity = await dbContext.Set<T>().FirstOrDefaultAsync(x => x.Key == Id);
                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();
            });

            return Json(apiMsg);
        }
    }
}
