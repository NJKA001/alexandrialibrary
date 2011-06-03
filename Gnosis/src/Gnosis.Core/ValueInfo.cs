using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Gnosis.Core
{
    public class ValueInfo
    {
        public ValueInfo(PropertyInfo property, Type valueType, string prefix)
        {
            this.property = property;
            this.name = string.Format("{0}_{1}", prefix, property.Name);

            GetElements(valueType);
        }

        private readonly PropertyInfo property;
        private readonly string name;
        private readonly List<ElementInfo> elements = new List<ElementInfo>();

        private void GetElements(Type type)
        {
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var ignore = false;
                foreach (var attribute in property.GetCustomAttributes(true))
                {
                    if (attribute is PersistenceIgnoreAttribute)
                    {
                        ignore = true;
                        break;
                    }
                }

                if (!ignore)
                {
                    if (property.PropertyType.IsPrimitive || property.PropertyType.IsSimple() || property.PropertyType.IsChildType())
                    {
                        elements.Add(new ElementInfo(property));
                    }
                }
            }
        }

        public string Name
        {
            get { return name; }
        }

        public ElementInfo Identifer
        {
            get { return elements.Where(x => x.Name == "Id").FirstOrDefault(); }
        }

        public ElementInfo ParentIdentifier
        {
            get { return elements.Where(x => x.Name == "Parent").FirstOrDefault(); }
        }

        public ElementInfo Sequence
        {
            get { return elements.Where(x => x.Name == "Sequence").FirstOrDefault(); }
        }

        public IEnumerable<ElementInfo> Elements
        {
            get { return elements; }
        }
    }
}
