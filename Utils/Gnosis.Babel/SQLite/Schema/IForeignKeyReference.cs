using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IForeignKeyReference : IStatement, IForeignKeyReferencable, ITableConstrained
    {
    }

    public interface IForeignKeyReference<T, TR> : IStatement, IForeignKeyReferencable<T, TR>, ITableConstrained<T>
    {
    }
}
