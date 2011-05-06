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
        public Set(IContext context, IEnumerable<T> items)
        {
            this.context = context;

            foreach (var item in items)
            {
                AddItem(item);
            }
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(OrderedSet<T>));

        private readonly IContext context;
        //private readonly IList<T> list = new List<T>();
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
                    log.Error("OrderedSet.OnCollectionChanged", ex);
                }
            }
        }

        protected IEnumerable<T> Items
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
            }
            else throw new ArgumentException("item already contained in set - a set cannot have duplicates");
        }

        #region ISet<T> Members

        public bool IsChanged
        {
            get { throw new NotImplementedException(); }
        }

        public void ResetState()
        {
            throw new NotImplementedException();
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void Replace(T original, T replacement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetExistingItems()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAddedItems()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetRemovedItems()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tuple<T, T>> GetReplacedItems()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region INotifyCollectionChanged Members

        public event System.Collections.Specialized.NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion
    }
}
