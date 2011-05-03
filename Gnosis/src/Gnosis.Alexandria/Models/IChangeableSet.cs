using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public interface IChangeableSet<T>
        : IEnumerable<T>, INotifyCollectionChanged
    {
        bool IsChanged { get; }

        void ClearState();
        void Add(T item);
        void Remove(T item);
        void Replace(T original, T replacement);

        IEnumerable<T> GetExistingItems();
        IEnumerable<T> GetAddedItems();
        IEnumerable<T> GetRemovedItems();
    }
}
