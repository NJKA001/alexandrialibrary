using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace Gnosis.Alexandria.Models
{
    public interface IChangeableOrderedSet<T>
        : IOrderedSet<T>,
        IChangeableSet<T>
    {
        void Insert(int index, T item);
        void Move(int index, T item);
        void RemoveAt(int index);

        IEnumerable<T> GetMovedItems();
    }
}
