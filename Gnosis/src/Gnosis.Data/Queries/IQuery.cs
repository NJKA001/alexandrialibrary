using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Data.Queries
{
    public interface IQuery<T>
        where T : IEntity
    {
        IEnumerable<T> Execute(IDbConnection connection);
    }
}
