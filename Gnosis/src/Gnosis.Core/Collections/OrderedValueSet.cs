using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Collections
{
    public class OrderedValueSet<T>
        : OrderedSet<T>, IOrderedValueSet<T> where T : IValue
    {
        public OrderedValueSet(IContext context)
            : base(context)
        {
        }

        public OrderedValueSet(IContext context, IEnumerable<T> items)
            : base(context, items)
        {
        }
    }
}
