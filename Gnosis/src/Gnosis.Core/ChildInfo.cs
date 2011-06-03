using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Gnosis.Core
{
    public class ChildInfo
    {
        public ChildInfo(PropertyInfo property, Type entityType, string prefix)
        {
            this.property = property;
            this.name = string.Format("{0}_{1}", prefix, property.Name);
            this.entity = new EntityInfo(entityType);
        }

        private readonly PropertyInfo property;
        private readonly string name;
        private readonly EntityInfo entity;

        public string Name
        {
            get { return name; }
        }

        public ElementInfo ParentIdentifier
        {
            get { return entity.Elements.Where(x => x.Name == "Parent").FirstOrDefault(); }
        }

        public ElementInfo Sequence
        {
            get { return entity.Elements.Where(x => x.Name == "Sequence").FirstOrDefault(); }
        }

        public EntityInfo Entity
        {
            get { return entity; }
        }
    }
}
