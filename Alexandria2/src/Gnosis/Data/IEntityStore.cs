using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface IEntityStore<T>
        where T : IEntity
    {
        void Initialize(Action<uint, T> entityLoaded, Action<uint, ILink, uint> linkLoaded, Action<uint, ITag, uint> tagLoaded);

        uint CreateEntity(IBatch<T> batch, T entity);
        uint CreateLink(IBatch<T> batch, ILink link, uint entityId);
        uint CreateTag(IBatch<T> batch, ITag tag, uint entityId);

        void DeleteEntity(IBatch<T> batch, uint id);
        void DeleteLink(IBatch<T> batch, uint id);
        void DeleteTag(IBatch<T> batch, uint id);

        void SaveEntity(IBatch<T> batch, uint id, T entity);
        void SaveLink(IBatch<T> batch, uint id, ILink link, uint entityId);
        void SaveTag(IBatch<T> batch, uint id, ITag tag, uint entityId);

        IBatch<T> CreateBatch(IEntityCache<T> cache);
    }
}
