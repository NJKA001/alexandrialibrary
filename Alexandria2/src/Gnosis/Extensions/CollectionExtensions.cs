using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public static class CollectionExtensions
    {
        public static void AddIfNotNull<T>(this ICollection<T> self, T item)
        {
            if (self != null && item != null)
                self.Add(item);
        }
    }
}
