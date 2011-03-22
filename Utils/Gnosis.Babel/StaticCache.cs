using System.Collections.Generic;

namespace Gnosis.Babel
{
    public class StaticCache<T> : ICache<T>
    {
        public StaticCache()
        {
        }

        private readonly IDictionary<object, T> _map = new Dictionary<object, T>();

        public bool IsEmpty
        {
            get { return _map.Count == 0; }
        }

        public bool IsFull
        {
            get { return false; }
        }

        public T GetOne(object id)
        {
            return (_map.ContainsKey(id)) ? _map[id] : default(T);
        }

        public ICollection<T> GetAll()
        {
            return _map.Values;
        }

        public void Put(object id, T model)
        {
            _map[id] = model;
        }
    }
}
