﻿using Microsoft.Extensions.Caching.Distributed;
using Oryx.Saas.Framework.Applications.WebSockets.Infrastructure;
using Oryx.Saas.Framework.Database.RabbitMQ;
using System.Text;

namespace Oryx.Saas.Framework.Applications.WebSockets.RabbitMQ
{
    public class SocialMsgManager
    {
        public const string SocialMsgWSRabbitMQKey = "SocialMsgWSRabbitMQ";

        public const string SocialMsgWSRabbitMQExchange = "SocialMsgWSRabbitMQExchange";

        public const string SocialMsgWSClientKey = "userId";

        private readonly IDistributedCache distributedCache;

        private readonly OryxWebSocketPool wsPool;

        private readonly RabbitMQClient rabbitMQClient;
         
        public SocialMsgManager(
            IDistributedCache _distributedCache,
            OryxWebSocketPool _wsPool,
            RabbitMQClient _rabbitMQClient
            )
        {
            distributedCache = _distributedCache;
            wsPool = _wsPool;
            rabbitMQClient = _rabbitMQClient;
        }

        public void RegisterWSRM()
        {
            wsPool.OnWebSocketAdd += WsPool_OnWebSocketAdd;
            wsPool.OnWebSocketRemove += WsPool_OnWebSocketRemove;
        }

        private void WsPool_OnWebSocketRemove(OryxWebSocketPoolItem wsItem)
        {
            var userId = wsItem.QueryString[SocialMsgWSClientKey];
            distributedCache.RemoveAsync(userId);
        }

        private void WsPool_OnWebSocketAdd(OryxWebSocketPoolItem wsItem)
        {
            //rabbitMQClient.
            var cacheWsKey = rabbitMQClient.GeneralKey(SocialMsgWSRabbitMQKey);
            distributedCache.SetStringAsync(wsItem.QueryString[SocialMsgWSClientKey].ToString(), cacheWsKey);
        }

        public bool HasOnline(string userId)
        {
            var userIdValue = distributedCache.GetString(userId);
            if (!string.IsNullOrEmpty(userIdValue))
            {
                return true;
            }
            return false;
        }

        public void SendMssage(string userId, string content)
        {
            var queue = rabbitMQClient[SocialMsgWSRabbitMQKey].Queue;
            var msgByts = Encoding.UTF8.GetBytes(content);
            queue.BasicPublish(SocialMsgWSRabbitMQExchange, userId, false, null, msgByts);
        }
    }
}
