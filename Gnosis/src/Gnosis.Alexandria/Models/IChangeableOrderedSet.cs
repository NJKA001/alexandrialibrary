using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public interface IChangeableOrderedSet<T>
        : IOrderedSet<T>,
        INotifyCollectionChanged
    {
        bool IsChanged { get; }
        IEnumerable<T> RemovedItems { get; }

        void ClearRemovedItems();
        void Insert(int index, T item);
        void InsertFirst(T item);
        void InsertLast(T item);
        void Remove(T item);
        void RemoveAt(int index);
    }
}
