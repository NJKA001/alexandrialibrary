using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Collections
{
    public interface IOrderedSet<T>
        : IEnumerable<T>, ISet<T>
    {
        T this[int index] { get; }

        void Insert(int index, T item);
        void Move(int index, T item);
        void RemoveAt(int index);

        int IndexOf(T item);
        //IEnumerable<T> GetMovedItems();
    }
}
