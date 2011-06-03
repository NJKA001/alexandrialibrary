using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Batches
{
    public class DeleteEntitiesBatch<T>
        : PersistEntitiesBatch
        where T : IEntity
    {
        public DeleteEntitiesBatch(Func<IDbConnection> getConnection, IEnumerable<T> entities)
            : base(getConnection)
        {
            var entityInfo = new EntityInfo(typeof(T));

            foreach (var entity in entities)
            {
                AddEntityDeleteStatement(entity, entityInfo);
            }
        }
    }
}
