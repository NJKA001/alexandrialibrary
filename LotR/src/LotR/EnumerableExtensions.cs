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
                throw new ArgumentException("cannot get a random element from an an empty list");

            if (list.Count == 1)
                return list.First();

            return list[random.Next(0, list.Count)];
        }
    }
}
