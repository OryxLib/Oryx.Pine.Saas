using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Saas.Framework.Database.RabbitMQ
{
    public class RabbitMQClient
    {
        private readonly string EnvID;
        private Dictionary<string, IModel> QueueDictionary = new Dictionary<string, IModel>();
        private Dictionary<string, EventingBasicConsumer> ConsumerDictionary = new Dictionary<string, EventingBasicConsumer>();
        //private ConnectionFactory factory;

        private readonly string rabbitHost;
        private readonly string rabbitUserName;
        private readonly string rabbitPassword;
        public RabbitMQClient(IConfiguration configuration)
        {
            rabbitHost = configuration["Database.RabbitMQ.Host"];
            rabbitUserName = configuration["Database.RabbitMQ.UserName"];
            rabbitPassword = configuration["Database.RabbitMQ.Password"];
            var distributeNo = configuration["Infrastructure.Distribute"];
            //创建连接工厂
            //引入全局应用ID
            EnvID = distributeNo;
        }

        /// <summary>
        /// 通过key名获取
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public RabbitMInstance this[string queueName]
        {
            get
            {
                var rabbitMInstance = new RabbitMInstance();
                rabbitMInstance.Queue = QueueDictionary[queueName + EnvID];
                rabbitMInstance.Consumer = ConsumerDictionary[queueName + EnvID];
                return rabbitMInstance;
            }
        }

        public string GeneralKey(string queueName)
        {
            return queueName + EnvID.ToString();
        }

        /// <summary>
        /// 注册接收队列
        /// </summary>
        /// <param name="queueName">队列名, 附加了全局应用ID</param>
        /// <param name="exchangeName">交换器名</param>
        public void RegisterReciverQueue(string queueName, string exchangeName)
        {
            var factory = new ConnectionFactory
            {
                UserName = rabbitUserName,//用户名
                Password = rabbitPassword,//密码
                HostName = rabbitHost//rabbitmq ip
            };
            var queueKey = GeneralKey(queueName);

            if (QueueDictionary.ContainsKey(queueKey))
            {
                return;
            }
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, false, true, null);
            channel.QueueDeclare(queueKey, false, false, false, null);
            channel.QueueBind(queueKey, exchangeName, string.Empty, null);
            QueueDictionary.Add(queueKey, channel);
        }

        public void RegisterQueueConsumer(string queueName)
        {
            var factory = new ConnectionFactory
            {
                UserName = rabbitUserName,//用户名
                Password = rabbitPassword,//密码
                HostName = rabbitHost//rabbitmq ip
            };
            var queueKey = queueName + EnvID;
            if (ConsumerDictionary.ContainsKey(queueKey))
            {
                return;
            }
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queueKey, false, consumer);
            ConsumerDictionary.Add(queueKey, consumer);
        }
    }

    public class RabbitMInstance
    {
        public IModel Queue { get; set; }

        public EventingBasicConsumer Consumer { get; set; }
    }
}
