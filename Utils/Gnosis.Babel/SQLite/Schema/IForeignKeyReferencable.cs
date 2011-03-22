using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IForeignKeyReferencable
    {
        IForeignKeyReference Column(string name);
    }

    public interface IForeignKeyReferencable<T, TR>
    {
        IForeignKeyReference<T, TR> Column(Expression<Func<TR, object>> expression);
    }
}
