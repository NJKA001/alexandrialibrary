using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Gnosis.Core
{
    public class EntityInfo
    {
        public EntityInfo(Type type)
        {
            name = GetEntityName(type);

            foreach (var interfaceType in type.GetInterfaces())
            {
                MapProperties(interfaceType);
            }

            MapProperties(type);
        }

        private readonly string name;
        private readonly IList<ElementInfo> elements = new List<ElementInfo>();
        private readonly IList<DataTypeInfo> dataTypes = new List<DataTypeInfo>();
        private readonly IList<ChildInfo> children = new List<ChildInfo>();
        private readonly IList<ValueInfo> values = new List<ValueInfo>();

        private static string GetEntityName(Type type)
        {
            if (type.IsInterface)
            {
                if (type.Name.StartsWith("I") && type.Name.Length > 1)
                    return type.Name.Substring(1);
            }

            return type.Name;
        }

        private void MapProperties(Type type)
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
                    else
                    {
                        var args = property.PropertyType.GetGenericArguments();
                        if (args != null && args.Length > 0)
                        {
                            var itemType = args[0];
                            if (itemType.IsChildType())
                            {
                                children.Add(new ChildInfo(property, itemType, name));
                            }
                            else if (itemType.IsValueType())
                            {
                                values.Add(new ValueInfo(property, itemType, name));
                            }
                        }
                        else
                        {
                            dataTypes.Add(new DataTypeInfo(property));
                        }
                    }
                }
            }
        }

        public string Name
        {
            get { return name; }
        }

        public ElementInfo Identifier
        {
            get { return elements.Where(x => x.Name == "Id").FirstOrDefault(); }
        }

        public IEnumerable<ElementInfo> Elements
        {
            get { return elements; }
        }

        public IEnumerable<DataTypeInfo> DataTypes
        {
            get { return dataTypes; }
        }

        public IEnumerable<ChildInfo> Children
        {
            get { return children; }
        }

        public IEnumerable<ValueInfo> Values
        {
            get { return values; }
        }
    }
}
