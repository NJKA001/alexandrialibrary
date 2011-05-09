using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using log4net;

namespace Gnosis.Alexandria.Models
{
    public class Set<T> : ISet<T>
    {
        public Set(IContext context)
        {
            this.context = context;
        }

        public Set(IContext context, IEnumerable<T> items)
        {
            this.context = context;

            foreach (var item in items)
            {
                map.Add(item.GetHashCode(), item);
                originalItems.Add(item);
            }
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(OrderedSet<T>));

        private readonly IContext context;
        private readonly IDictionary<int, T> map = new Dictionary<int, T>();
        private readonly IList<T> originalItems = new List<T>();
        private readonly IList<T> addedItems = new List<T>();
        private readonly IList<T> removedItems = new List<T>();
        private readonly IList<Tuple<T, T>> replacedItems = new List<Tuple<T, T>>();

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
                    log.Error("Set.OnCollectionChanged", ex);
                }
            }
        }

        protected IContext Context
        {
            get { return context; }
        }

        protected IList<T> OriginalItems
        {
            get { return originalItems; }
        }

        protected IDictionary<int, T> Map
        {
            get { return map; }
        }

        protected virtual IEnumerable<T> Items
        {
            get { return map.Values; }
        }

        protected void AddItem(T item)
        {
            var key = item.GetHashCode();
            if (!map.ContainsKey(key))
            {
                var action = new Action(delegate { map.Add(key, item); });
                OnCollectionChanged(action, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
                addedItems.Add(item);
            }
            else throw new ArgumentException("item already contained in set - a set cannot have duplicates");
        }

        protected void RemoveItem(T item)
        {
            var key = item.GetHashCode();
            if (map.ContainsKey(key))
            {
                var action = new Action(delegate { map.Remove(key); });
                OnCollectionChanged(action, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
                removedItems.Add(item);
            }
            else throw new KeyNotFoundException("item not contained in set - cannot remove");
        }

        protected void ReplaceItem(T original, T replacement)
        {
            var key = original.GetHashCode();
            if (map.ContainsKey(key))
            {
                var action = new Action(delegate { map.Remove(key); map.Add(replacement.GetHashCode(), replacement); });
                OnCollectionChanged(action, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, replacement, original));
                replacedItems.Add(new Tuple<T, T>(original, replacement));
            }
            else throw new KeyNotFoundException("item not contained in set - cannot replace");
        }

        #region ISet<T> Members

        public int Count
        {
            get { return map.Count; }
        }

        public bool IsChanged
        {
            get { return addedItems.Count > 0 || removedItems.Count > 0 || replacedItems.Count > 0; }
        }

        public virtual void Clear()
        {
            var action = new Action(delegate { map.Clear(); });
            OnCollectionChanged(action, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void ResetState()
        {
            addedItems.Clear();
            removedItems.Clear();
            replacedItems.Clear();

            originalItems.Clear();
            foreach (var item in Items)
                originalItems.Add(item);
        }

        public virtual void Add(T item)
        {
            AddItem(item);
        }

        public virtual void Remove(T item)
        {
            RemoveItem(item);
        }

        public virtual void Replace(T original, T replacement)
        {
            ReplaceItem(original, replacement);
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

        public bool Contains(T item)
        {
            return map.ContainsKey(item.GetHashCode());
        }

        public IEnumerable<T> GetAddedItems()
        {
            return addedItems;
        }

        public IEnumerable<T> GetRemovedItems()
        {
            return removedItems;
        }

        public IEnumerable<Tuple<T, T>> GetReplacedItems()
        {
            return replacedItems;
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        #endregion

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion
    }
}
