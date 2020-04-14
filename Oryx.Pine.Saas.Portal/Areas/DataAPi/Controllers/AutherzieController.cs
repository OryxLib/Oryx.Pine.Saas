using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oryx.Saas.Framework.Business;
using Oryx.Saas.Framework.ViewModel;

namespace Oryx.Pine.Saas.Portal.Areas.DataAPi.Controllers
{
    [Area("DataAPi")]
    [Authorize]
    public class AppDataController : Controller
    {
        MongoClient mongo;
        SaasServiceDbContext dbContext;
        public AppDataController(MongoClient _mongo, SaasServiceDbContext _dbContext)
        {
            mongo = _mongo;
            dbContext = _dbContext;
        }
         
        public async Task<IActionResult> GetTableList(string AppToken)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var dataList = await dbContext.AppStructs.FirstOrDefaultAsync(x => x.AppId == AppToken);
                return dataList;
            });

            return Json(apiMsg);
        }

        public async Task<IActionResult> GetDataOne(string AppToken, string AppTable, string query)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await mongo.GetDatabase(AppToken)
                   .GetCollection<BsonDocument>(AppTable)
                   .Find(filter: BsonDocument
                   .Parse(query))
                   .FirstOrDefaultAsync();
            });

            return Json(apiMsg);
        }

        public async Task<IActionResult> GetDataList(string AppToken, string AppTable, string query, int page = 1, int size = 10)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var queryable = mongo.GetDatabase(AppToken)
                   .GetCollection<BsonDocument>(AppTable)
                   .Find(filter: BsonDocument
                   .Parse(query));

                var count = await queryable.CountDocumentsAsync();
                var data = await queryable.Skip((page - 1) * 10).Limit(10).ToListAsync();
                return new { data, count };
            });

            return Json(apiMsg);
        }

        public async Task<IActionResult> Add(string AppToken, string AppTable, object data)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var jsonString = JsonConvert.SerializeObject(data);
                var queryable = mongo.GetDatabase(AppToken)
                   .GetCollection<BsonDocument>(AppTable)
                   .InsertOneAsync(BsonDocument.Parse(jsonString));
            });

            return Json(apiMsg);
        }
        public async Task<IActionResult> AddMany(string AppToken, string AppTable, object data)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var jarr = JArray.FromObject(data);
                var bl = new List<BsonDocument>();
                foreach (var item in jarr)
                {
                    bl.Add(BsonDocument.Parse(item.ToString()));
                }
                var queryable = mongo.GetDatabase(AppToken)
                   .GetCollection<BsonDocument>(AppTable)
                   .InsertManyAsync(bl);
            });
            return Json(apiMsg);
        }
        public async Task<IActionResult> Upate(string AppToken, string AppTable, object data, string query)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var jsonString = JsonConvert.SerializeObject(data);
                var queryable = mongo.GetDatabase(AppToken)
                   .GetCollection<BsonDocument>(AppTable)
                   .UpdateOneAsync(filter: BsonDocument.Parse(query), update: BsonDocument.Parse(jsonString));
            });

            return Json(apiMsg);
        }
        public async Task<IActionResult> UpateMany(string AppToken, string AppTable, object data, string query)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var jsonString = JsonConvert.SerializeObject(data);
                var queryable = await mongo.GetDatabase(AppToken)
                   .GetCollection<BsonDocument>(AppTable)
                   .UpdateOneAsync(filter: BsonDocument.Parse(query), update: BsonDocument.Parse(jsonString));
            });

            return Json(apiMsg);
        }
        public async Task<IActionResult> Delete(string AppToken, string AppTable, string query)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var queryable = await mongo.GetDatabase(AppToken)
                   .GetCollection<BsonDocument>(AppTable)
                   .DeleteOneAsync(filter: BsonDocument.Parse(query));
            });

            return Json(apiMsg);
        }

        public async Task<IActionResult> DeleteMany(string AppToken, string AppTable, string query)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var queryable = await mongo.GetDatabase(AppToken)
                   .GetCollection<BsonDocument>(AppTable)
                   .DeleteManyAsync(filter: BsonDocument.Parse(query));
            });

            return Json(apiMsg);
        }
    }
}