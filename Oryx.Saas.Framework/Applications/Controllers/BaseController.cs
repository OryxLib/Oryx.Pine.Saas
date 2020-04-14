using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
namespace Oryx.Saas.Framework.Applications.Controllers
{
    public class BaseController : Microsoft.AspNetCore.Mvc.Controller
    {
        DbContext dbContext { get; set; }

        public Dictionary<string, string> Config { get; set; }

        public BaseController(DbContext _dbContext)
        {
            dbContext = _dbContext;
            Config = new Dictionary<string, string>();
            //InitBaseController();
        }

        //private void InitBaseController()
        //{
        //    var configEntiry = dbContext.Queryable<ConfigEntry>().First();

        //    MapDataToDctionary<ConfigEntry>(configEntiry);
        //}

        private void MapDataToDctionary<T>(object data)
        {
            if (data == null)
            {
                return;
            }
            //var newDictionary = new Dictionary<string, string>();
            var _type = typeof(T);
            var properties = _type.GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(data).ToString();
                var name = prop.Name;
                Config.Add(name, value);
            }
        }
    }
}
