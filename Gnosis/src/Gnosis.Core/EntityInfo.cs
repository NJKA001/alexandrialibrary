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
            this.name = GetEntityName(type);
            this.type = type;

            MapTypes(type);
        }

        public EntityInfo(Type type, EntityInfo parent)
        {
            this.name = GetEntityName(type);
            this.type = type;
            this.parent = parent;

            MapTypes(type);
        }

        private readonly string name;
        private readonly Type type;
        private readonly EntityInfo parent;
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

        private void MapTypes(Type type)
        {
            foreach (var interfaceType in type.GetInterfaces())
            {
                MapProperties(interfaceType);
            }

            MapProperties(type);
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
                                children.Add(new ChildInfo(this, property, itemType, name));
                            }
                            else if (itemType.IsValueType())
                            {
                                values.Add(new ValueInfo(this, property, itemType, name));
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

        public Type Type
        {
            get { return type; }
        }

        public EntityInfo Parent
        {
            get { return parent; }
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
