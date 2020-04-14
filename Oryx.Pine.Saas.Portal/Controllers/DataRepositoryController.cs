using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Oryx.Pine.Saas.Portal.Controllers
{
    [ApiController]
    [Route("/api/DataRepository")]
    [Authorize]
    public class DataRepositoryController : Controller
    {
        ILogger logger;
        MongoClient mongoClient;
        IMongoDatabase mdb;
        public DataRepositoryController(ILogger _logger, MongoClient _mongoClient)
        {
            logger = _logger;
            mongoClient = _mongoClient;
            mdb = mongoClient.GetDatabase("Default");
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(string table, string query)
        {
            var data = mdb.GetCollection<BsonDocument>(table);
            data.Find(new BsonDocument()).Project("{_id:0}").ToList();
            var filter = Builders<BsonDocument>.Filter.Eq(query, 29000);
            var doc = await data.Find(filter).FirstOrDefaultAsync();
            return Json(doc);
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post(string table, BsonDocument data)
        {
            var mtable = mdb.GetCollection<BsonDocument>(table);
            await mtable.InsertOneAsync(data);
            return Json(new { success = true });
        }

        [HttpPut]
        [Route("HttpPut")]
        public async Task Update(string table, BsonDocument data, string query)
        {
            var mtable = mdb.GetCollection<BsonDocument>(table);
            var filter = Builders<BsonDocument>.Filter.Eq(query, query);
            var update = Builders<BsonDocument>.Update.Set("name", "1");

            mtable.UpdateOne(filter, update);
        }
    }
}