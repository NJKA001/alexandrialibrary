using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public interface ICache<T>
        where T : Entity
    {
        void Add(uint id, T entity);
        void Add(uint id, T entity, Link link);
        void Add(uint id, T entity, Tag tag);

        void Remove(T entity);
        void Remove(T entity, Link link);
        void Remove(T entity, Tag tag);

        uint GetId(T entity);
        uint GetId(Link link);
        uint GetId(Tag tag);

        T GetEntity(uint id);
        Link GetLink(uint id);
        Tag GetTag(uint id);
        IEnumerable<Link> GetLinksFor(uint entityId);
        IEnumerable<Tag> GetTagsFor(uint entityId);
    }
}
