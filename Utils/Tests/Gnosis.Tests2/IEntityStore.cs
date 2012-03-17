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
        void Initialize(Action entityAction, Action linkAction, Action tagAction);
        
        IChange DeleteEntity(uint id);
        IChange DeleteLink(uint id);
        IChange DeleteTag(uint id);

        IChange SaveEntity(uint id, T entity);
        IChange SaveLink(uint id, Link link, uint entityId);
        IChange SaveTag(uint id, Tag tag, uint entityId);

        IBatch<T> CreateBatch();
    }
}
