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
                tagsByEntityId[entityId] = new List<Tag> { tag };
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

        public void Remove(T entity, Link link)
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

        public void Remove(T entity, Tag tag)
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

        public uint GetId(Link link)
        {
            if (link == null)
                return 0;

            return linksById.Where(x => x.Value == link).FirstOrDefault().Key;
        }

        public uint GetId(Tag tag)
        {
            if (tag == null)
                return 0;

            return tagsById.Where(x => x.Value == tag).FirstOrDefault().Key;
        }

        public T GetEntity(uint id)
        {
            return entitiesById.ContainsKey(id) ? entitiesById[id] : null;
        }

        public Link GetLink(uint id)
        {
            return linksById.ContainsKey(id) ? linksById[id] : null;
        }

        public Tag GetTag(uint id)
        {
            return tagsById.ContainsKey(id) ? tagsById[id] : null;
        }

        public IEnumerable<Link> GetLinksFor(uint entityId)
        {
            return linksByEntityId.ContainsKey(entityId) ? linksByEntityId[entityId].ToList() : Enumerable.Empty<Link>();
        }

        public IEnumerable<Tag> GetTagsFor(uint entityId)
        {
            return tagsByEntityId.ContainsKey(entityId) ? tagsByEntityId[entityId].ToList() : Enumerable.Empty<Tag>();
        }
    }
}
