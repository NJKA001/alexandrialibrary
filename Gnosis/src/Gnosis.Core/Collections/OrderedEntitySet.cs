using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Collections
{
    public class OrderedEntitySet<T>
        : OrderedSet<T>, IOrderedEntitySet<T> where T : IEntity
    {
        public OrderedEntitySet(IContext context)
            : base(context)
        {
        }

        public OrderedEntitySet(IContext context, IEnumerable<T> items)
            : base(context, items)
        {
        }
    }
}
