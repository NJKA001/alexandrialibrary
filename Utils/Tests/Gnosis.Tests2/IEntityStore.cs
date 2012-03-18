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
        void Initialize(Action<uint, T> entityLoaded, Action<uint, Link, uint> linkLoaded, Action<uint, Tag, uint> tagLoaded);

        uint CreateEntity(IBatch<T> batch, T entity);
        uint CreateLink(IBatch<T> batch, Link link, uint entityId);
        uint CreateTag(IBatch<T> batch, Tag tag, uint entityId);

        void DeleteEntity(IBatch<T> batch, uint id);
        void DeleteLink(IBatch<T> batch, uint id);
        void DeleteTag(IBatch<T> batch, uint id);

        void SaveEntity(IBatch<T> batch, uint id, T entity);
        void SaveLink(IBatch<T> batch, uint id, Link link, uint entityId);
        void SaveTag(IBatch<T> batch, uint id, Tag tag, uint entityId);

        IBatch<T> CreateBatch(ICache<T> cache);
    }
}
