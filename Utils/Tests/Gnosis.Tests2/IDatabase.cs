using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public interface IDatabase<T>
        where T : Entity
    {
        void Initialize(Action entityAction, Action linkAction, Action tagAction);
        
        IDbCommand DeleteEntity(IDbConnection connection, uint id);
        IDbCommand DeleteLink(IDbConnection connection, uint id);
        IDbCommand DeleteTag(IDbConnection connection, uint id);

        IDbCommand InsertEntity(IDbConnection connection, T entity);
        IDbCommand InsertLink(IDbConnection connection, Link link, uint entityId);
        IDbCommand InsertTag(IDbConnection connection, Tag tag, uint entityId);
        
        IDbCommand UpdateEntity(IDbConnection connection, T entity, uint id);
        IDbCommand UpdateLink(IDbConnection connection, Link link, uint id, uint entityId);
        IDbCommand UpdateTag(IDbConnection connection, Tag tag, uint id, uint entityId);
    }
}
