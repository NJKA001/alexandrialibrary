using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace Gnosis.Alexandria.Models
{
    public class ChangeableOrderedSet<T>
        : OrderedSet<T>, IChangeableOrderedSet<T>
    {
        public ChangeableOrderedSet()
            : base(new List<T>())
        {
        }

        public ChangeableOrderedSet(IEnumerable<T> items)
            : base(items)
        {
            foreach (var item in items)
                originalItems.Add(item);
        }

        private readonly IList<T> originalItems = new List<T>();
        private readonly IList<T> addedItems = new List<T>();
        private readonly IList<T> removedItems = new List<T>();

        protected void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, args);
        }

        public bool IsChanged
        {
            get { return addedItems.Count > 0 || removedItems.Count > 0; }
        }

        public void ClearState()
        {
            addedItems.Clear();
            removedItems.Clear();

            originalItems.Clear();
            foreach (var item in Items)
                originalItems.Add(item);
        }

        public new void Add(T item)
        {
            base.Add(item);
            addedItems.Add(item);
        }

        public new void Insert(int index, T item)
        {
            base.Insert(index, item);
            addedItems.Add(item);
        }

        public new void Move(int index, T item)
        {
            base.Move(index, item);
        }

        public new void Remove(T item)
        {
            base.Remove(item);
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < Count)
            {
                var item = base[index];
                base.Remove(item);
            }
            else throw new IndexOutOfRangeException();
        }

        public new void Replace(T original, T replacement)
        {
            base.Replace(original, replacement);
            addedItems.Add(replacement);
            removedItems.Add(original);
        }

        public IEnumerable<T> GetAddedItems()
        {
            return addedItems;
        }

        public IEnumerable<T> GetMovedItems()
        {
            IList<T> movedItems = new List<T>();

            var index = 0;
            foreach (var item in Items)
            {
                if (originalItems.Count > index)
                {
                    if (originalItems[index].GetHashCode() != item.GetHashCode())
                        movedItems.Add(item);
                }
                else movedItems.Add(item);

                index++;
            }

            return movedItems;
        }

        public IEnumerable<T> GetRemovedItems()
        {
            return removedItems;
        }

        public new System.Collections.IEnumerator GetEnumerator()
        {
            return base.GetEnumerator();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
