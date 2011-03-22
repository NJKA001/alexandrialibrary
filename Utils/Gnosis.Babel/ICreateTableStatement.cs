using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel
{
    public interface ICreateTableStatement<T> : IStatement
        where T : IModel
    {
        T Model { get; set; }
        ICreateTableStatement<T> Temporary { get; }
        ICreateTableStatement<T> IfNotExists { get; }
        ICreateTableStatement<T> PrimaryKey(Expression<Func<T, object>> property);
        ICreateTableStatement<T> Column(Expression<Func<T, object>> property);
        ICreateTableStatement<T> Columns(IEnumerable<Expression<Func<T, object>>> properties);
    }
}
