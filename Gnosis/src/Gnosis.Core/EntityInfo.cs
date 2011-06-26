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
            this.name = type.ToPersistentName();
            this.type = type;

            MapTypes(type);
        }

        public EntityInfo(Type type, EntityInfo parent)
        {
            this.name = type.ToPersistentName();
            this.type = type;
            this.parent = parent;

            MapTypes(type);
        }

        private readonly string name;
        private readonly Type type;
        private readonly EntityInfo parent;
        private readonly IList<ElementInfo> elements = new List<ElementInfo>();
        private readonly IList<DataTypeInfo> dataTypes = new List<DataTypeInfo>();
        private readonly IList<EntityInfo> children = new List<EntityInfo>();
        private readonly IList<ValueInfo> values = new List<ValueInfo>();

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
                    if (property.PropertyType.IsPrimitive || property.PropertyType.IsSimpleType() || property.PropertyType.IsChildType())
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
                                children.Add(new EntityInfo(itemType, this));
                            }
                            else if (itemType.IsValueType())
                            {
                                values.Add(new ValueInfo(this, property, itemType));
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

        public bool IsRoot
        {
            get { return parent == null; }
        }

        public EntityInfo Parent
        {
            get { return parent; }
        }

        public ElementInfo Identifier
        {
            get { return elements.Where(elem => elem.IsPrimaryKey).FirstOrDefault(); }
        }

        public ElementInfo ParentIdentifier
        {
            get { return elements.Where(x => x.Name == "Parent").FirstOrDefault(); }
        }

        public ElementInfo Sequence
        {
            get { return elements.Where(x => x.Name == "Sequence").FirstOrDefault(); }
        }

        public ElementInfo TimeStamp
        {
            get { return elements.Where(elem => elem.IsTimeStamp).FirstOrDefault(); }
        }

        public IEnumerable<ElementInfo> Elements
        {
            get { return elements; }
        }

        public IEnumerable<DataTypeInfo> DataTypes
        {
            get { return dataTypes; }
        }

        public IEnumerable<EntityInfo> Children
        {
            get { return children; }
        }

        public IEnumerable<ValueInfo> Values
        {
            get { return values; }
        }
    }
}
