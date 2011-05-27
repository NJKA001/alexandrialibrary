using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Batches
{
    public class SelectEntitiesBatch<T>
        : Batch
        where T : IEntity
    {
        public SelectEntitiesBatch(Func<IDbConnection> getConnection, IEnumerable<T> entities)
            : base(getConnection)
        {
        }
    }
}
