using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface IDeleteStatement<T> : IStatement, IFilterable<T>
        where T : IModel
    {
    }
}
