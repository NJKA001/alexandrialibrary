using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Queries
{
    public interface IValueQuery<T>
        where T : IValue
    {
        IEnumerable<T> Execute();
    }
}
