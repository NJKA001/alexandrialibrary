using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Gnosis.Core
{
    public class DataTypeInfo
    {
        public DataTypeInfo(PropertyInfo property)
        {
            foreach (var subProperty in property.PropertyType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                elements.Add(new ElementInfo(property, property.Name));
            }
        }

        private readonly PropertyInfo property;
        private readonly IList<ElementInfo> elements = new List<ElementInfo>();

        public IEnumerable<ElementInfo> Elements
        {
            get { return elements; }
        }

        public object GetValue(ElementInfo element, object instance)
        {
            if (elements.Contains(element))
            {
                var dataType = property.GetValue(instance, null);
                if (dataType != null)
                    return element.GetValue(dataType);
            }

            return null;
        }
    }
}
