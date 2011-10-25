using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> self, Func<TSource, TKey> getKey)
        {
            var keys = new HashSet<TKey>();
            foreach (var item in self)
                if (keys.Add(getKey(item)))
                    yield return item;
        }
    }
}
