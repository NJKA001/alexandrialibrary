using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Gnosis.Core
{
    public class ColumnInfo
    {
        public ColumnInfo(string name, PropertyInfo property)
        {
            this.name = name;
            this.property = property;
        }

        private readonly string name;
        private readonly PropertyInfo property;

        public string Name
        {
            get { return name; }
        }

        public bool IsReadOnly
        {
            get { return !property.CanWrite; }
        }

        public object GetValue(object instance)
        {
            return property.GetValue(instance, null);
        }

        public void SetValue(object instance, object value)
        {
            if (!IsReadOnly)
            {
                property.SetValue(instance, value, null);
            }
        }
    }
}
