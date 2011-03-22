using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel
{
    public interface IInsertStatement<T> : IStatement
        where T : IModel
    {
        IInsertStatement<T> Column(Expression<Func<T, object>> property);
        IInsertStatement<T> Columns(IEnumerable<Expression<Func<T, object>>> properties);
    }
}
