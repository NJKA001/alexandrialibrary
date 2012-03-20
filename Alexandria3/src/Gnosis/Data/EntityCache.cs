using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public class EntityCache<T>
        : IEntityCache<T>
        where T : class, IEntity
    {
        private readonly ObservableCollection<T> entities = new ObservableCollection<T>();
        private readonly IDictionary<uint, T> entitiesById = new Dictionary<uint, T>();
        private readonly IDictionary<uint, ILink> linksById = new Dictionary<uint, ILink>();
        private readonly IDictionary<uint, ITag> tagsById = new Dictionary<uint, ITag>();
        private readonly IDictionary<uint, IList<ILink>> linksByEntityId = new Dictionary<uint, IList<ILink>>();
        private readonly IDictionary<uint, IList<ITag>> tagsByEntityId = new Dictionary<uint, IList<ITag>>();

        public IEnumerable<T> Entities
        {
            get { return entities; }
        }

        public void Add(uint id, T entity)
        {
            if (id == 0)
                throw new ArgumentException("id cannot be zero");

            if (entitiesById.ContainsKey(id))
                return;

            entitiesById.Add(id, entity);
            entities.Add(entity);
        }

        public void Add(uint id, T entity, ILink link)
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
                linksByEntityId[entityId] = new List<ILink> { link };
        }

        public void Add(uint id, T entity, ITag tag)
        {
            if (tagsById.ContainsKey(id))
                return;

            var entityId = GetId(entity);
            if (entityId == 0)
                return;

            tagsById.Add(id, tag);

            if (tagsByEntityId.ContainsKey(entityId))
            {
                if (tagsByEntityId[entityId].Contains(tag))
                    return;

                tagsByEntityId[entityId].Add(tag);
            }
            else
                tagsByEntityId[entityId] = new List<ITag> { tag };
        }

        public void Remove(T entity)
        {
            var id = GetId(entity);
            if (id == 0)
                return;

            foreach (var link in entity.Links)
            {
                Remove(entity, link);
            }

            foreach (var tag in entity.Tags)
            {
                Remove(entity, tag);
            }

            if (!entitiesById.ContainsKey(id))
                return;

            if (entities.Contains(entity))
                entities.Remove(entity);

            entitiesById.Remove(id);
        }

        public void Remove(T entity, ILink link)
        {
            var id = GetId(link);
            if (id == 0)
                return;

            if (!linksById.ContainsKey(id))
                return;

            var entityId = GetId(entity);
            if (linksByEntityId.ContainsKey(entityId))
            {
                if (linksByEntityId[entityId].Contains(link))
                    linksByEntityId[entityId].Remove(link);
            }

            linksById.Remove(id);
        }

        public void Remove(T entity, ITag tag)
        {
            var id = GetId(tag);
            if (id == 0)
                return;

            if (!tagsById.ContainsKey(id))
                return;

            var entityId = GetId(entity);
            if (tagsByEntityId.ContainsKey(entityId))
            {
                if (tagsByEntityId[entityId].Contains(tag))
                    tagsByEntityId[entityId].Remove(tag);
            }

            tagsById.Remove(id);
        }

        public uint GetId(T entity)
        {
            if (entity == null)
                return 0;

            return entitiesById.Where(x => x.Value == entity).FirstOrDefault().Key;
        }

        public uint GetId(ILink link)
        {
            if (link == null)
                return 0;

            return linksById.Where(x => x.Value == link).FirstOrDefault().Key;
        }

        public uint GetId(ITag tag)
        {
            if (tag == null)
                return 0;

            return tagsById.Where(x => x.Value == tag).FirstOrDefault().Key;
        }

        public T GetEntity(uint id)
        {
            return entitiesById.ContainsKey(id) ? entitiesById[id] : null;
        }

        public ILink GetLink(uint id)
        {
            return linksById.ContainsKey(id) ? linksById[id] : null;
        }

        public ITag GetTag(uint id)
        {
            return tagsById.ContainsKey(id) ? tagsById[id] : null;
        }

        public IEnumerable<ILink> GetLinksFor(uint entityId)
        {
            return linksByEntityId.ContainsKey(entityId) ? linksByEntityId[entityId].ToList() : Enumerable.Empty<ILink>();
        }

        public IEnumerable<ITag> GetTagsFor(uint entityId)
        {
            return tagsByEntityId.ContainsKey(entityId) ? tagsByEntityId[entityId].ToList() : Enumerable.Empty<ITag>();
        }
    }
}
