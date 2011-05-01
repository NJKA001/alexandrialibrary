using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public interface IOrderedSet<T>
        : IEnumerable<T>
    {
        int Count { get; }
        T this[int index] { get; }

        bool Contains(T item);
        int IndexOf(T item);
    }
}
