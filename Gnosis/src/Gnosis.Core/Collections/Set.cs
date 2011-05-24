using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Collections
{
    public class Set<T> : Set<int, T>
    {
        public Set(IContext context)
            : base(context, x => x.GetHashCode())
        {
        }

        public Set(IContext context, IEnumerable<T> items)
            : base(context, x => x.GetHashCode(), items)
        {
        }
    }

    public class Set<K, V> : ISet<V>
    {
        public Set(IContext context, Func<V, K> function)
        {
            this.context = context;
            this.function = function;
        }

        public Set(IContext context, Func<V, K> function, IEnumerable<V> items)
        {
            this.context = context;
            this.function = function;

            foreach (var item in items)
            {
                map.Add(GetKey(item), item);
                originalItems.Add(item);
            }
        }

        private readonly IContext context;
        private readonly IDictionary<K, V> map = new Dictionary<K, V>();
        private readonly Func<V, K> function;
        private readonly IList<V> originalItems = new List<V>();
        private readonly IList<V> removedItems = new List<V>();

        protected void OnCollectionChanged(Action action, NotifyCollectionChangedEventArgs args)
        {
            context.Invoke(action);

            if (CollectionChanged != null)
            {
                CollectionChanged(this, args);
            }
        }

        protected IContext Context
        {
            get { return context; }
        }

        protected IList<V> OriginalItems
        {
            get { return originalItems; }
        }

        protected IList<V> RemovedItems
        {
            get { return removedItems; }
        }

        protected IDictionary<K, V> Map
        {
            get { return map; }
        }

        protected virtual IEnumerable<V> Items
        {
            get { return map.Values; }
        }

        protected K GetKey(V value)
        {
            return function(value);
        }

        protected void AddItem(V item)
        {
            var key = GetKey(item);
            if (!map.ContainsKey(key))
            {
                var action = new Action(delegate { map.Add(key, item); });
                OnCollectionChanged(action, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            }
            else throw new ArgumentException("item already contained in set - a set cannot have duplicates");
        }

        protected void RemoveItem(V item)
        {
            var key = GetKey(item);
            if (map.ContainsKey(key))
            {
                var action = new Action(delegate { map.Remove(key); removedItems.Add(item); });
                OnCollectionChanged(action, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            }
            else throw new KeyNotFoundException("item not contained in set - cannot remove");
        }

        protected void ReplaceItem(V original, V replacement)
        {
            var key = GetKey(original);
            if (map.ContainsKey(key))
            {
                var replacementKey = GetKey(replacement);
                var action = new Action(delegate { map.Remove(key); map.Add(replacementKey, replacement); });
                OnCollectionChanged(action, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, replacement, original));
            }
            else throw new KeyNotFoundException("item not contained in set - cannot replace");
        }

        #region ISet Members

        public virtual IEnumerable<CollectionItemInfo> GetItemInfo()
        {
            var info = new List<CollectionItemInfo>();

            foreach (var currentItem in Items)
            {
                if (originalItems.Contains(currentItem))
                {
                    info.Add(new CollectionItemInfo(currentItem, CollectionItemState.Existing, GetKey(currentItem)));
                }
                else
                {
                    info.Add(new CollectionItemInfo(currentItem, CollectionItemState.Added, GetKey(currentItem))); 
                }
            }

            foreach (var removedItem in removedItems)
            {
                var removedKey = GetKey(removedItem);
                if (!Map.ContainsKey(removedKey))
                {
                    info.Add(new CollectionItemInfo(removedItem, CollectionItemState.Removed, removedKey));
                }
            }

            return info;
        }

        #endregion

        #region ISet<V> Members

        public int Count
        {
            get { return map.Count; }
        }

        public virtual bool IsChanged
        {
            get
            {
                if (originalItems.Count != map.Count)
                    return true;

                foreach (var item in originalItems)
                {
                    if (!map.ContainsKey(GetKey(item)))
                        return true;
                }

                return false;
            }
        }

        public virtual void Clear()
        {
            var action = new Action(delegate { map.Clear(); });
            OnCollectionChanged(action, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void ResetState()
        {
            removedItems.Clear();
            originalItems.Clear();

            foreach (var item in Items)
                originalItems.Add(item);
        }

        public virtual void Add(V item)
        {
            AddItem(item);
        }

        public virtual void Remove(V item)
        {
            RemoveItem(item);
        }

        public virtual void Replace(V original, V replacement)
        {
            ReplaceItem(original, replacement);
        }

        public bool Contains(V item)
        {
            return map.ContainsKey(GetKey(item));
        }

        #endregion

        #region IEnumerable<V> Members

        public IEnumerator<V> GetEnumerator()
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
