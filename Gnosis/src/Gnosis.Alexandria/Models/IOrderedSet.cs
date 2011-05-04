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

        void Insert(int index, T item);
        void Move(int index, T item);
        void RemoveAt(int index);

        bool Contains(T item);
        int IndexOf(T item);
        IEnumerable<T> GetMovedItems();
    }
}
