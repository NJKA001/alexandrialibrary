using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public static class EnumerableExtensions
    {
        public static bool ContainsEntityOrId<T>(this IEnumerable<T> sequence, T entity)
            where T : IEntity
        {
            return sequence.Contains(entity) || sequence.Where(x => x.Id == entity.Id).FirstOrDefault() != null;
        }

        public static bool ContainsValueOrId<T>(this IEnumerable<T> sequence, T value)
            where T : IValue
        {
            return sequence.Contains(value) || sequence.Where(x => x.Id == value.Id).FirstOrDefault() != null;
        }

        public static string ToNamesString<T>(this IEnumerable<T> self)
        {
            if (self == null || self.Count() == 0)
                return string.Empty;

            return string.Join("; ", self);
        }

        public static string ToNamesString<T>(this IEnumerable<T> self, Func<T, string> function)
        {
            if (self == null || self.Count() == 0)
                return string.Empty;

            var values = self.Select(function);

            return string.Join("; ", values);
        }
    }
}
