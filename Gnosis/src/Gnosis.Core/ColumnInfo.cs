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
            : this(name, property, false, false)
        {
        }

        public ColumnInfo(string name, PropertyInfo property, bool isPrimaryKey, bool isAutoIncrement)
        {
            this.name = name;
            this.property = property;
            this.isPrimaryKey = isPrimaryKey;
        }

        private readonly string name;
        private readonly PropertyInfo property;
        private readonly bool isPrimaryKey;
        private readonly bool isAutoIncrement;

        public string Name
        {
            get { return name; }
        }

        public bool IsPrimaryKey
        {
            get { return isPrimaryKey; }
        }

        public bool IsAutoIncrement
        {
            get { return isAutoIncrement; }
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
