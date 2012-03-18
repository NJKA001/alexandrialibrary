using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public interface IEntityStore<T>
        where T : Entity
    {
        void Initialize(Action<T> entityLoaded, Action<Link> linkLoaded, Action<Tag> tagLoaded);
        
        void DeleteEntity(IBatch<T> batch, uint id);
        void DeleteLink(IBatch<T> batch, uint id);
        void DeleteTag(IBatch<T> batch, uint id);

        void SaveEntity(IBatch<T> batch, uint id, T entity, Action<uint> entityCreated);
        void SaveLink(IBatch<T> batch, uint id, Link link, uint entityId, Action<uint> linkCreated);
        void SaveTag(IBatch<T> batch, uint id, Tag tag, uint entityId, Action<uint> tagCreated);

        IBatch<T> CreateBatch(ICache<T> cache);
    }
}
