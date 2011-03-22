using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel
{
    public interface IUpdateStatement<T> : IStatement, IFilterable<T>
        where T : IModel
    {
        IUpdateStatement<T> Column(Expression<Func<T, object>> property);
        IUpdateStatement<T> Columns(IEnumerable<Expression<Func<T, object>>> properties);
    }
}
