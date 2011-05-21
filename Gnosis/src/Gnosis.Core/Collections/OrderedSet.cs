using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace Gnosis.Core.Collections
{
    public class OrderedSet<T>
        : Set<T>, IOrderedSet<T>
    {
        public OrderedSet(IContext context)
            : base(context)
        {
        }

        public OrderedSet(IContext context, IEnumerable<T> items)
            : base(context,  items)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    list.Add(item);
                }
            }
        }

        private readonly IList<T> list = new List<T>();

        protected override IEnumerable<T> Items
        {
            get { return list; }
        }

        protected void InsertItem(int index, T item)
        {
            var key = GetKey(item);
            if (!Map.ContainsKey(key))
            {
                var action = new Action(delegate { list.Insert(index, item); AddItem(item); });
                OnCollectionChanged(action, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
            }
            else throw new ArgumentException("item already contained in set - a set cannot have duplicates");
        }

        protected void MoveItem(int index, T item)
        {
            var key = GetKey(item);
            if (Map.ContainsKey(key))
            {
                var oldIndex = IndexOf(item);
                var action = new Action(delegate { list.Remove(item); list.Insert(index, item); });
                OnCollectionChanged(action, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, item, index, oldIndex));
            }
            else throw new KeyNotFoundException("item not contained in set - cannot move");
        }

        public T this[int index]
        {
            get { return list[index]; }
        }

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public override void Clear()
        {
            var action = new Action(delegate { list.Clear(); Map.Clear(); });
            OnCollectionChanged(action, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public override void Add(T item)
        {
            list.Add(item);
            AddItem(item);
        }

        public void Insert(int index, T item)
        {
            InsertItem(index, item);
        }

        public void Move(int index, T item)
        {
            MoveItem(index, item);
        }

        public override void Remove(T item)
        {
            list.Remove(item);
            RemoveItem(item);
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < Count)
            {
                var item = list[index];
                list.RemoveAt(index);
                RemoveItem(item);
            }
            else throw new IndexOutOfRangeException();
        }

        public override void Replace(T original, T replacement)
        {
            list.Insert(list.IndexOf(original), replacement);
            list.Remove(original);
            ReplaceItem(original, replacement);
        }

        public IEnumerable<T> GetMovedItems()
        {
            IList<T> movedItems = new List<T>();

            var index = 0;
            foreach (var item in Items)
            {
                if (OriginalItems.Count > index)
                {
                    if (GetKey(OriginalItems[index]).Equals(GetKey(item)))
                        movedItems.Add(item);
                }
                else movedItems.Add(item);

                index++;
            }

            return movedItems;
        }

        #region ISet Members

        public override IEnumerable<CollectionItemInfo> GetItemInfo()
        {
            var info = new List<CollectionItemInfo>();

            foreach (var added in GetAddedItems())
                info.Add(new CollectionItemInfo(IndexOf(added), added, CollectionItemState.Added));

            foreach (var removed in GetRemovedItems())
                info.Add(new CollectionItemInfo(IndexOf(removed), removed, CollectionItemState.Removed));

            var movedItems = GetMovedItems();
            foreach (var existing in GetExistingItems())
            {
                if (movedItems.Contains(existing))
                    info.Add(new CollectionItemInfo(IndexOf(existing), existing, CollectionItemState.Moved));
                else
                    info.Add(new CollectionItemInfo(IndexOf(existing), existing, CollectionItemState.Existing));
            }

            return info;
        }

        #endregion
    }
}
