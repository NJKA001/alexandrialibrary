using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Data.Batches
{
    public class DeleteEntitiesBatch<T>
        : PersistEntitiesBatch
        where T : IEntity
    {
        public DeleteEntitiesBatch(IDbConnection connection, IEnumerable<T> entities)
            : base(connection)
        {
            var entityInfo = new EntityInfo(typeof(T));

            foreach (var entity in entities)
            {
                AddEntityDeleteStatement(entity, entityInfo);
            }
        }
    }
}
