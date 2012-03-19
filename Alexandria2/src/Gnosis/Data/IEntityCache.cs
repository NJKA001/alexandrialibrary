using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface IEntityCache<T>
        where T : IEntity
    {
        IEnumerable<T> Entities { get; }

        void Add(uint id, T entity);
        void Add(uint id, T entity, ILink link);
        void Add(uint id, T entity, ITag tag);

        void Remove(T entity);
        void Remove(T entity, ILink link);
        void Remove(T entity, ITag tag);

        uint GetId(T entity);
        uint GetId(ILink link);
        uint GetId(ITag tag);

        T GetEntity(uint id);
        ILink GetLink(uint id);
        ITag GetTag(uint id);
        IEnumerable<ILink> GetLinksFor(uint entityId);
        IEnumerable<ITag> GetTagsFor(uint entityId);
    }
}
