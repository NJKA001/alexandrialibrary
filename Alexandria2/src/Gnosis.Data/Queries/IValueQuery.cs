using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Data.Queries
{
    public interface IValueQuery<T>
        where T : IValue
    {
        IEnumerable<T> Execute(IDbConnection connection);
    }
}
