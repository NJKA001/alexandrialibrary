using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel
{
    public interface ISelectStatement<T> : IStatement
        where T : IModel
    {
        ISelectStatement<T> All { get; }
        ISelectStatement<T> Distinct { get; }
        ISelectStatement<T> Column(Expression<Func<T, object>> property);
        ISelectStatement<T> Column(Expression<Func<T, object>> property, string alias);
        ISelectStatement<T> Column(string function, string alias);
        ISelectStatement<T> Columns(IEnumerable<Expression<Func<T, object>>> properties);
        ISelectStatement<T> GroupBy(Expression<Func<T, object>> property);
        ISelectStatement<T> OrderByAsc(Expression<Func<T, object>> property);
        ISelectStatement<T> OrderByDesc(Expression<Func<T, object>> property);
    }
}
