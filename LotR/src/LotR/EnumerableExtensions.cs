using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public static class EnumerableExtensions
    {
        public static T GetRandomItem<T>(this IEnumerable<T> self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var random = new Random();
            var list = self.ToList();

            if (list.Count == 0)
                return default(T);

            return list[random.Next(0, list.Count)];
        }
    }
}
