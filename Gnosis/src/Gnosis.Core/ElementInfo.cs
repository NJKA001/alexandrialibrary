using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Gnosis.Core
{
    public class ElementInfo
    {
        public ElementInfo(PropertyInfo property)
        {
            this.property = property;
            this.name = property.Name;
        }

        public ElementInfo(PropertyInfo property, string prefix)
        {
            this.property = property;
            this.name = prefix + property.Name;
        }

        private readonly PropertyInfo property;
        private readonly string name;

        public string Name
        {
            get { return name; }
        }

        public bool IsAutoIncrement
        {
            get { return false; }
        }

        public bool IsPrimaryKey
        {
            get { return property.Name == "Id" && property.PropertyType == typeof(Guid); }
        }

        public bool IsReadOnly
        {
            get { return !property.CanWrite; }
        }

        public Type Type
        {
            get { return property.PropertyType; }
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
