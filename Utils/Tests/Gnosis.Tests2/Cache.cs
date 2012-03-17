using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class Cache<T>
        : ICache<T>
        where T : Entity
    {
        private readonly ObservableCollection<T> entities = new ObservableCollection<T>();
        private readonly IDictionary<uint, T> entitiesById = new Dictionary<uint, T>();
        private readonly IDictionary<uint, Link> linksById = new Dictionary<uint, Link>();
        private readonly IDictionary<uint, Tag> tagsById = new Dictionary<uint, Tag>();
        private readonly IDictionary<uint, IList<Link>> linksByEntityId = new Dictionary<uint, IList<Link>>();
        private readonly IDictionary<uint, IList<Tag>> tagsByEntityId = new Dictionary<uint, IList<Tag>>();

        public void Add(uint id, T entity)
        {
            if (id == 0)
                throw new ArgumentException("id cannot be zero");

            if (entitiesById.ContainsKey(id))
                return;

            entitiesById.Add(id, entity);
            entities.Add(entity);
        }

        public void Add(uint id, T entity, Link link)
        {
            if (linksById.ContainsKey(id))
                return;

            var entityId = GetId(entity);
            if (entityId == 0)
                return;

            linksById.Add(id, link);

            if (linksByEntityId.ContainsKey(entityId))
            {
                if (linksByEntityId[entityId].Contains(link))
                    return;

                linksByEntityId[entityId].Add(link);
            }
            else
                linksByEntityId[entityId] = new List<Link> { link };
        }

        public void Add(uint id, T entity, Tag tag)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity, Link link)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity, Tag tag)
        {
            throw new NotImplementedException();
        }

        public uint GetId(T entity)
        {
            throw new NotImplementedException();
        }

        public uint GetId(T entity, Link link)
        {
            throw new NotImplementedException();
        }

        public uint GetId(T entity, Tag tag)
        {
            throw new NotImplementedException();
        }

        public T GetEntity(uint id)
        {
            throw new NotImplementedException();
        }

        public Link GetLink(uint id)
        {
            throw new NotImplementedException();
        }

        public Tag GetTag(uint id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Link> GetLinksFor(uint entityId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tag> GetTagsFor(uint entityId)
        {
            throw new NotImplementedException();
        }
    }
}
