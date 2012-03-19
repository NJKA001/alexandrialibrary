using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Data.Queries
{
    public interface IOutlineQuery<TEntity, TOutline>
        where TEntity : IEntity
        where TOutline: IOutline<TEntity>
    {
        IEnumerable<TOutline> Execute(IDbConnection connection);
    }
}
