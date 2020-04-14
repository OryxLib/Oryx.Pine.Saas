using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Saas.Framework.Model
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ModelType : Attribute
    {
        public ModelType()
        {

        }
        public string Name { get; set; }

        public ControlType ControlType { get; set; }

        /// <summary>
        /// 与DataSourceTable  方式互斥
        /// 格式 value1,value2,value3
        /// </summary>
        public string DataSource { get; set; }

        public bool Required { get; set; }

        /// <summary>
        /// 数据源:对应数据库查询的表名
        /// </summary>
        public string DataSourceTable { get; set; }

        /// <summary>
        /// 前端显示的文本对应表的字段
        /// </summary>
        public string DataSourceTableValue { get; set; }

        /// <summary>
        /// 查询表的where 表达式
        /// </summary>
        public string DataSourceQuery { get; set; }

        public bool ShowOnList { get; set; } = true;

        public int Order { get; set; } = 0;
    }


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ModelBindData : Attribute
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
