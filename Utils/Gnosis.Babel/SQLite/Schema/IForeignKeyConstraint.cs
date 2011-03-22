using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IForeignKeyConstraint : IStatement, IForeignKeyColumnar
    {
    }

    public interface IForeignKeyConstraint<T> : IStatement, IForeignKeyColumnar<T>
    {
    }
}
