using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IForeignKeyReferenceConstraint : IStatement, IForeignKeyReferencable
    {
    }

    public interface IForeignKeyReferenceConstraint<T, TR> : IStatement, IForeignKeyReferencable<T, TR>
    {
    }
}
