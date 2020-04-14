using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oryx.Saas.Framework.Business;
using Oryx.Saas.Framework.Business.Entities;
using Oryx.Saas.Framework.Business.ServiceSection.SaasServiceSection;
using Oryx.Saas.Framework.Model;
using Oryx.Saas.Framework.ViewModel;
using Oryx.Utilities.ValueType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Saas.Framework.Applications.Controllers.MongoDb
{

    public class BaseMongoController : Controller
    {
        MongoClient mongo;
        SaasServiceDbContext dbContext;
        ModelMapper modelMapper;
        public BaseMongoController(MongoClient client, SaasServiceDbContext _dbContext)
        {
            mongo = client;
            dbContext = _dbContext;
            modelMapper = new ModelMapper(dbContext);
        }

        public async Task<IActionResult> Index(Guid appHeaderId, Guid appStructId, int page = 1, int size = 10)
        {
            var itemList = await dbContext.AppStructItem
                 .Where(x => x.AppHeaderId == appHeaderId && x.AppStructId == appStructId)
                 .ToListAsync();

            ViewData["ModelTable"] = DataExtension.StrucItemToModelTable(itemList);
            return View();
        }
        public async Task<IActionResult> List(Guid appHeaderId, Guid appStructId, string query, int page = 1, int size = 10)
        {
            var itemList = await dbContext.AppStructItem
                   .Where(x => x.AppHeaderId == appHeaderId && x.AppStructId == appStructId)
                   .ToListAsync();

            ApiMessage apiMsg;
            var code = 0;
            long count = 0;
            List<BsonDocument> data = null;
            var msg = "";
            var appHeaderIdStr = appHeaderId.ToString();
            var appStructIdStr = appStructId.ToString();
            try
            {
                var collection = mongo.GetDatabase(appHeaderIdStr).GetCollection<BsonDocument>(appStructIdStr);
                var doc = collection.Find(new BsonDocument());
                if (!string.IsNullOrEmpty(query))
                {
                    doc = doc
                        .Project(query);
                }

                count = await doc.CountDocumentsAsync();
                data = await doc
                        .Skip((page - 1) * size)
                        .Limit(size)
                        .ToListAsync();
            }
            catch (Exception exc)
            {
                msg = exc.Message;
            }
            var dataJson = data.ToJobj();
            return Content("{\"code\":" + code + ",\"count\":" + count + ",\"data\":" + dataJson.ToString() + ",\"msg\":\"" + msg + "\"}", "application/json");
        }
        public async Task<IActionResult> AddOrUpdate(Guid appHeaderId, Guid appStructId, string Id)
        {
            var itemList = await dbContext.AppStructItem
                  .Where(x => x.AppHeaderId == appHeaderId && x.AppStructId == appStructId)
                  .ToListAsync();
            var tmp = DataExtension.StrucItemToModelType(itemList);
            tmp.Add(new ModelInfo
            {
                PropName = "hide",
                Name = "_id"
            });
            ViewData["ModelType"] = tmp;

            var appHeaderIdStr = appHeaderId.ToString();
            var appStructIdStr = appStructId.ToString();

            if (!string.IsNullOrEmpty(Id))
            {
                //new QueryComplete();
                var collection = mongo.GetDatabase(appHeaderIdStr).GetCollection<BsonDocument>(appStructIdStr);
                var data = await collection.Find(new BsonDocument("_id", ObjectId.Parse(Id))).FirstAsync();
                ViewData["ModelData"] = data.BsonToModelData();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(Guid appHeaderId, Guid appStructId)
        {
            var itemList = await dbContext.AppStructItem
                  .Where(x => x.AppHeaderId == appHeaderId && x.AppStructId == appStructId)
                  .ToListAsync();

            var tmp = DataExtension.StrucItemToModelType(itemList);
            tmp.Add(new ModelInfo
            {
                PropName = "hide",
                Name = "_id"
            });
            ViewData["ModelType"] = tmp;
            var appHeaderIdStr = appHeaderId.ToString();
            var appStructIdStr = appStructId.ToString();
            var entityData = HttpContext.Request.Form.GetBson();
            var entity = HttpContext.Request.Form.GetJson();
            var id = entity["_id"]?.ToString();
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var collection = mongo.GetDatabase(appHeaderIdStr).GetCollection<BsonDocument>(appStructIdStr);

                if (string.IsNullOrEmpty(id) || id == Guid.Empty.ToString())
                {

                    await collection.InsertOneAsync(entityData);
                }
                else
                {
                    await collection.FindOneAndUpdateAsync(filter: new BsonDocument("_id", ObjectId.Parse(id)), update: entityData);
                }
            });
            return Json(apiMsg);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid appHeaderId, Guid appStructId, string Id)
        {
            var appHeaderIdStr = appHeaderId.ToString();
            var appStructIdStr = appStructId.ToString();
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var collection = mongo.GetDatabase(appHeaderIdStr).GetCollection<BsonDocument>(appStructIdStr);

                await collection.DeleteOneAsync(new BsonDocument("_id", ObjectId.Parse(Id)));
            });
            return Json(apiMsg);
        }
    }
}
