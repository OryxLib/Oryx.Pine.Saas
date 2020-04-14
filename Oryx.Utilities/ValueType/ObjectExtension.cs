using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oryx.Utilities.ValueType
{
    public static class ObjectExtension
    {
        public static object GetValue(this object obj, string key)
        {
            var type = obj.GetType();
            var prop = type.GetProperties().FirstOrDefault(x => x.Name.ToLower() == key.ToLower());
            if (prop != null)
            {
                return prop.GetValue(obj);
            }
            return null;
        }
    }
}
