using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using GnosisTests.Entities;

namespace GnosisTests
{
    public class EntityCache<T>
        : IEntityCache<T>
        where T : IEntity
    {
        private readonly ObservableCollection<T> entities = new ObservableCollection<T>();
        private readonly Dictionary<uint, T> byId = new Dictionary<uint, T>();

        public IEnumerable<T> Entities
        {
            get { return entities; }
        }

        public T Get(uint id)
        {
            return byId.ContainsKey(id) ? byId[id] : default(T);
        }

        public void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (byId.ContainsKey(entity.Id))
                return;

            entities.Add(entity);
            byId.Add(entity.Id, entity);
        }

        public void Remove(uint id)
        {
            if (!byId.ContainsKey(id))
                return;

            var entity = byId[id];
            entities.Remove(entity);
            byId.Remove(id);
        }
    }
}
