using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Gnosis.Core
{
    public class ChildInfo
    {
        public ChildInfo(PropertyInfo property, Type entityType)
        {
            this.property = property;
            this.entity = new EntityInfo(entityType);
        }

        private readonly PropertyInfo property;
        private readonly EntityInfo entity;

        public string Name
        {
            get { return property.Name; }
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
