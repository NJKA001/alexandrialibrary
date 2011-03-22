using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IKeyConstraint : IStatement, IKeyColumnar
    {
    }

    public interface IKeyConstraint<T> : IStatement, IKeyColumnar<T>
    {
    }
}
