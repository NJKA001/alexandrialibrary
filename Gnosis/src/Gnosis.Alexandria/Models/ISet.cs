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
        bool IsChanged { get; }

        void ResetState();
        void Add(T item);
        void Remove(T item);
        void Replace(T original, T replacement);

        IEnumerable<T> GetExistingItems();
        IEnumerable<T> GetAddedItems();
        IEnumerable<T> GetRemovedItems();
    }
}
