using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Gnosis.Core
{
    public class ColumnInfo
    {
        public ColumnInfo(string name, PropertyInfo property, object defaultValue)
        {
            this.name = name;
            this.property = property;
            this.defaultValue = defaultValue;
            this.isPrimaryKey = false;
            this.isAutoIncrement = false;
        }

        public ColumnInfo(string name, PropertyInfo property, bool isPrimaryKey, bool isAutoIncrement)
        {
            this.name = name;
            this.property = property;
            this.defaultValue = null;
            this.isPrimaryKey = isPrimaryKey;
            this.isAutoIncrement = isAutoIncrement;
        }

        private readonly string name;
        private readonly PropertyInfo property;
        private readonly object defaultValue;
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

        public Type ColumnType
        {
            get { return property.PropertyType; }
        }

        public object DefaultValue
        {
            get { return defaultValue; }
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

        public override string ToString()
        {
            if (isPrimaryKey)
                return string.Format("PRIMARY KEY {0} ({1})", name, ColumnType);
            else
                return string.Format("COLUMN {0} ({1})", name, ColumnType);
        }
    }
}
