using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Saas.Framework.Helpers.ConfigureOptions
{
    public class DatabaseOptions
    {
        /// <summary>
        /// Options : MysqlDB,Sqlite,SqlExpress
        /// </summary>
        public string DefaultRSDB { get; set; }

        public InfluxdDBItem InfluxdDB { get; set; } = default;

        public RabbitMQItem RabbitMQ { get; set; } = default;

        public RedisItem Redis { get; set; } = default;

        /// <summary>
        /// sqlExpress connectionstring
        /// </summary>
        public string MssqlDB { get; set; }

        /// <summary>
        /// MongoDB connectionstring
        /// </summary>
        public string MongoDb { get; set; }

        /// <summary>
        /// MysqlDB connectionstring
        /// </summary>
        public string MysqlDB { get; set; }

        public string Sqlite { get; set; }
    }

    public class InfluxdDBItem
    {
        public bool Eanbled { get; set; } = false;

        public string ConnectionString { get; set; }
    }

    public class RabbitMQItem
    {
        public bool Eanbled { get; set; } = false;

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }
    }

    public class RedisItem
    {
        public bool Eanbled { get; set; } = false;

        public string ConnectionString { get; set; }
    }
}
