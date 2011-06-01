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
            var table = typeof(T).GetTableInfo();

            foreach (var entity in entities)
            {
                AddEntityDeleteStatement(entity, table);

                foreach (var childInfo in table.Children)
                {
                    AddChildDeleteStatements(childInfo, entity);
                }
            }
        }
    }
}
