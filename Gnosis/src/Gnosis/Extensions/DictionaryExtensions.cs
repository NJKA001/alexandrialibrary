using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public static class DictionaryExtensions
    {
        public static void AddIfNotNull<T, TKey>(this IDictionary<TKey, string> self, TKey key, T item)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            self.AddIfNotNull(key, item, x => x.ToString());
        }

        public static void AddIfNotNull<T, TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, T item, Func<T, TValue> func)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            self.AddIfTrue(key, item, x => x != null, func);
        }

        public static void AddIfTrue<T, TKey>(this IDictionary<TKey, string> self, TKey key, T item, Predicate<T> condition)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            self.AddIfTrue(key, item, condition, x => x.ToString());
        }

        public static void AddIfTrue<T, TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, T item, Predicate<T> condition, Func<T, TValue> func)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (key == null)
                throw new ArgumentNullException("key");
            if (condition == null)
                throw new ArgumentNullException("condition");
            if (func == null)
                throw new ArgumentNullException("func");

            if (!condition(item))
                return;
            
            var value = func(item);
            self.Add(key, value);
        }
    }
}
