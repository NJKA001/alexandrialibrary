using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public interface ISet<T>
        : IEnumerable<T>, INotifyCollectionChanged
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
