using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Threading;

using log4net;

namespace Gnosis.Alexandria.Models
{
    public class OrderedSet<T> : IOrderedSet<T>
    {
        public OrderedSet(IContext context)
        {
            this.context = context;
        }

        public OrderedSet(IContext context, IEnumerable<T> items)
            : this(context)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    originalItems.Add(item);
                    AddItem(item);
                }
            }
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(OrderedSet<T>));

        private readonly IContext context;
        private readonly IList<T> list = new List<T>();
        private readonly IDictionary<int, T> map = new Dictionary<int, T>();
        private readonly IList<T> originalItems = new List<T>();
        private readonly IList<T> addedItems = new List<T>();
        private readonly IList<T> removedItems = new List<T>();

        protected void OnCollectionChanged(Action action, NotifyCollectionChangedEventArgs args)
        {
            context.Invoke(action);

            if (CollectionChanged != null)
            {
                try
                {
                    CollectionChanged(this, args);
                }
                catch (Exception ex)
                {
                    log.Error("OrderedSet.OnCollectionChanged", ex);
                }
            }
        }

        protected IEnumerable<T> Items
        {
            get { return list; }
        }

        protected void AddItem(T item)
        {
            var key = item.GetHashCode();
            if (!map.ContainsKey(key))
            {
                var action = new Action(delegate { list.Add(item); map.Add(key, item); });
                OnCollectionChanged(action, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            }
            else throw new ArgumentException("item already contained in set - a set cannot have duplicates");
        }

        protected void InsertItem(int index, T item)
        {
            var key = item.GetHashCode();
            if (!map.ContainsKey(key))
            {
                var action = new Action(delegate { list.Insert(index, item); map.Add(key, item); });
                OnCollectionChanged(action, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
            }
            else throw new ArgumentException("item already contained in set - a set cannot have duplicates");
        }

        protected void MoveItem(int index, T item)
        {
            var key = item.GetHashCode();
            if (map.ContainsKey(key))
            {
                var oldIndex = IndexOf(item);
                var action = new Action(delegate { list.Remove(item); list.Insert(index, item); });
                OnCollectionChanged(action, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, item, index, oldIndex));
            }
            else throw new KeyNotFoundException("item not contained in set - cannot move");
        }

        protected void RemoveItem(T item)
        {
            var key = item.GetHashCode();
            if (map.ContainsKey(key))
            {
                var action = new Action(delegate { list.Remove(item); map.Remove(key); });
                OnCollectionChanged(action, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            }
            else throw new KeyNotFoundException("item not contained in set - cannot remove");
        }

        protected void ReplaceItem(T original, T replacement)
        {
            var key = original.GetHashCode();
            if (map.ContainsKey(key))
            {
                var action = new Action(delegate { var index = IndexOf(original); list.Remove(original); map.Remove(key); list.Insert(index, replacement); map.Add(replacement.GetHashCode(), replacement); });
                OnCollectionChanged(action, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, replacement, original));
            }
            else throw new KeyNotFoundException("item not contained in set - cannot replace");
        }

        public int Count
        {
            get { return list.Count; }
        }

        public T this[int index]
        {
            get { return list[index]; }
        }

        public bool Contains(T item)
        {
            return map.ContainsKey(item.GetHashCode());
        }

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public bool IsChanged
        {
            get { return addedItems.Count > 0 || removedItems.Count > 0; }
        }

        public void ResetState()
        {
            addedItems.Clear();
            removedItems.Clear();

            originalItems.Clear();
            foreach (var item in Items)
                originalItems.Add(item);
        }

        public void Clear()
        {
            var action = new Action(delegate { list.Clear(); map.Clear(); });
            OnCollectionChanged(action, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void Add(T item)
        {
            AddItem(item);
            addedItems.Add(item);
        }

        public void Insert(int index, T item)
        {
            InsertItem(index, item);
            addedItems.Add(item);
        }

        public void Move(int index, T item)
        {
            MoveItem(index, item);
        }

        public void Remove(T item)
        {
            RemoveItem(item);
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < Count)
            {
                var item = list[index];
                RemoveItem(item);
            }
            else throw new IndexOutOfRangeException();
        }

        public void Replace(T original, T replacement)
        {
            ReplaceItem(original, replacement);
            addedItems.Add(replacement);
            removedItems.Add(original);
        }

        public IEnumerable<T> GetExistingItems()
        {
            IList<T> existingItems = new List<T>();

            var index = 0;
            foreach (var item in Items)
            {
                if (originalItems.Count > index)
                {
                    if (originalItems[index].GetHashCode() == item.GetHashCode())
                        existingItems.Add(item);
                }

                index++;
            }

            return existingItems;
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

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
