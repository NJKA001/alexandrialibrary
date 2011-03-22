using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel
{
    public interface ICreateIndexStatement<T> : IStatement
        where T : IModel
    {
        T Model { get; set; }
        ICreateIndexStatement<T> Unique { get; }
        ICreateIndexStatement<T> IfNotExists { get; }
        ICreateIndexStatement<T> Ascending(Expression<Func<T, object>> property);
        ICreateIndexStatement<T> Descending(Expression<Func<T, object>> property);
    }
}
