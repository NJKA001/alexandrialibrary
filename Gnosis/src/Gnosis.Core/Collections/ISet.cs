using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Collections
{
    public interface ISet
    {
        IEnumerable<CollectionItemInfo> GetItemInfo();
    }

    public interface ISet<T>
        : ISet, IEnumerable<T>, INotifyCollectionChanged
    {
        int Count { get; }
        bool IsChanged { get; }

        void Clear();
        void ResetState();
        void Add(T item);
        void Remove(T item);
        void Replace(T original, T replacement);

        bool Contains(T item);
        IEnumerable<T> GetExistingItems();
        IEnumerable<T> GetAddedItems();
        IEnumerable<T> GetRemovedItems();
        IEnumerable<Tuple<T, T>> GetReplacedItems();
    }
}
