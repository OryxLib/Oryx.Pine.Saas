using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oryx.Saas.Framework.Business.Entities;
using Oryx.Saas.Framework.Business.ServiceSection.SaasServiceSection.ServiceEntities;
using Oryx.Saas.Framework.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Saas.Framework.Business.ServiceSection.SaasServiceSection
{
    public static class DataExtension
    {
        public static ModelTable StrucItemToModelTable(List<AppStructItem> appStructItems)
        {
            var modelTable = new ModelTable();
            modelTable.TableInfoList = new List<TableInfo>();
            foreach (var item in appStructItems)
            {
                var tableInfo = new TableInfo();
                tableInfo.Name = item.Name;
                tableInfo.PropName = item.Type;
                modelTable.TableInfoList.Add(tableInfo);
            }

            return modelTable;
        }

        public static List<ModelInfo> StrucItemToModelType(List<AppStructItem> appStructItems)
        {
            var modelInfoList = new List<ModelInfo>();
            foreach (var item in appStructItems)
            {
                var tableInfo = new ModelInfo();
                tableInfo.Name = item.Name;
                tableInfo.PropName = item.Type;
                modelInfoList.Add(tableInfo);
            }

            return modelInfoList;
        }

        public static BsonDocument GetBson(this IFormCollection keyValuePairs)
        {
            var bson = new BsonDocument();
            foreach (var formItem in keyValuePairs)
            {
                if (formItem.Key != "_id")
                {
                    bson.Add(new BsonElement(formItem.Key, formItem.Value.ToString()));
                }
            }
            return bson;
        }

        public static JObject GetJson(this IFormCollection keyValuePairs)
        {
            var json = new JObject();
            foreach (var formItem in keyValuePairs)
            {
                json.Add(formItem.Key, JToken.FromObject(formItem.Value.ToString()));
            }
            return json;
        }

        public static ModelData BsonToModelData(this BsonDocument bson)
        {
            var modelData = new ModelData();

            foreach (var bi in bson)
            {
                if (bi.Value.IsBsonArray)
                {
                    modelData.Add(bi.Name, bi.Value[0].ToString());
                }
                else
                {
                    modelData.Add(bi.Name, bi.Value.ToString());
                }
            }

            return modelData;
        }
        public static JArray ToJobj(this List<BsonDocument> document)
        {
            var jarr = new JArray();
            foreach (var doc in document)
            {
                var jobject = new JObject();
                foreach (var ele in doc.Elements)
                {
                    if (ele.Value.IsBsonArray)
                    {
                        jobject.Add(ele.Name, JToken.FromObject(ele.Value[0]));
                    }
                    else
                    {
                        jobject.Add(ele.Name, JToken.FromObject(ele.Value));
                    }
                }
                jarr.Add(jobject);
            }
            //json.Add(doc.AsBsonMinKey.ToString(), JToken.FromObject(doc.AsBsonValue));
            return jarr;
        }

    }
}
