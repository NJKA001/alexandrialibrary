using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Collections
{
    public class EntitySet<T>
        : Set<T> where T : IEntity
    {
        public EntitySet(IContext context)
            : base(context)
        {
        }

        public EntitySet(IContext context, IEnumerable<T> items)
            : base(context, items)
        {
        }
    }
}
