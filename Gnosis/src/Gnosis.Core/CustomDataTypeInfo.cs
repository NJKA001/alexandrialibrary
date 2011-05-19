using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Gnosis.Core
{
    public class CustomDataTypeInfo
    {
        public CustomDataTypeInfo(IEnumerable<ColumnInfo> columns, PropertyInfo property)
        {
            this.columns = columns;
            this.property = property;
        }

        private readonly IEnumerable<ColumnInfo> columns;
        private readonly PropertyInfo property;

        public IEnumerable<ColumnInfo> Columns
        {
            get { return columns; }
        }

        public object GetValue(object instance)
        {
            return property.GetValue(instance, null);
        }

        public IEnumerable<KeyValuePair<string, object>> GetFields(object instance)
        {
            var values = new Dictionary<string, object>();

            var propertyValue = GetValue(instance);

            foreach (var column in columns)
                values.Add(column.Name, column.GetValue(propertyValue));

            return values;
        }
    }
}
