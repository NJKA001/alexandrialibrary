using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Gnosis.Core
{
    public class ChildInfo
    {
        public ChildInfo(EntityInfo parent, PropertyInfo property, Type itemType)
        {
            this.parent = parent;
            this.property = property;
            this.entity = new EntityInfo(itemType, parent);
        }

        private readonly EntityInfo parent;
        private readonly PropertyInfo property;
        private readonly EntityInfo entity;

        public ElementInfo ParentIdentifier
        {
            get { return entity.Elements.Where(x => x.Name == "Parent").FirstOrDefault(); }
        }

        public ElementInfo Sequence
        {
            get { return entity.Elements.Where(x => x.Name == "Sequence").FirstOrDefault(); }
        }

        public EntityInfo Parent
        {
            get { return parent; }
        }

        public EntityInfo Entity
        {
            get { return entity; }
        }
    }
}
