using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace Gnosis.Alexandria.Models
{
    public class OrderedSet<T> : IOrderedSet<T>
    {
        public OrderedSet(IEnumerable<T> items)
        {
            foreach (var item in items)
                AddItem(item);
        }

        private readonly Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
        private readonly IList<T> list = new List<T>();
        private readonly IDictionary<int, T> map = new Dictionary<int, T>();

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
                if (dispatcher.CheckAccess())
                {
                    action.Invoke();
                }
                else
                {
                    dispatcher.Invoke(action, DispatcherPriority.DataBind, null);
                }
            }
            else throw new ArgumentException("item already contained in set - a set cannot have duplicates");
        }

        protected void InsertItem(int index, T item)
        {
            var key = item.GetHashCode();
            if (!map.ContainsKey(key))
            {
                var action = new Action(delegate { list.Insert(index, item); map.Add(key, item); });
                if (dispatcher.CheckAccess())
                {
                    action.Invoke();
                }
                else
                {
                    dispatcher.Invoke(action, DispatcherPriority.DataBind, null);
                }
            }
            else throw new ArgumentException("item already contained in set - a set cannot have duplicates");
        }

        protected void MoveItem(int index, T item)
        {
            var key = item.GetHashCode();
            if (map.ContainsKey(key))
            {
                var action = new Action(delegate { list.Remove(item); list.Insert(index, item); });
                if (dispatcher.CheckAccess())
                {
                    action.Invoke();
                }
                else
                {
                    dispatcher.Invoke(action, DispatcherPriority.DataBind, null);
                }
            }
            else throw new KeyNotFoundException("item not contained in set - cannot move");
        }

        protected void RemoveItem(T item)
        {
            var key = item.GetHashCode();
            if (map.ContainsKey(key))
            {
                var action = new Action(delegate { list.Remove(item); map.Remove(key); });
                if (dispatcher.CheckAccess())
                {
                    action.Invoke();
                }
                else
                {
                    dispatcher.Invoke(action, DispatcherPriority.DataBind, null);
                }
            }
            else throw new KeyNotFoundException("item not contained in set - cannot remove");
        }

        protected void ReplaceItem(T original, T replacement)
        {
            var key = original.GetHashCode();
            if (map.ContainsKey(key))
            {
                var action = new Action(delegate { var index = IndexOf(original); list.Remove(original); map.Remove(key); list.Insert(index, replacement); map.Add(replacement.GetHashCode(), replacement); });
                if (dispatcher.CheckAccess())
                {
                    action.Invoke();
                }
                else
                {
                    dispatcher.Invoke(action, DispatcherPriority.DataBind, null);
                }
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
