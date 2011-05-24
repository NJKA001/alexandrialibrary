using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Collections
{
    public class ValueSet<T>
        : Set<T>, IValueSet<T> where T : IValue
    {
        public ValueSet(IContext context)
            : base(context)
        {
        }

        public ValueSet(IContext context, IEnumerable<T> items)
            : base(context, items)
        {
        }
    }
}
