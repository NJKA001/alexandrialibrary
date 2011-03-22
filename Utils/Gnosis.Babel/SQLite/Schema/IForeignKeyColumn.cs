using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IForeignKeyColumn : IStatement, IForeignKeyColumnar
    {
        IForeignKeyReferenceConstraint References(string table);
    }

    public interface IForeignKeyColumn<T> : IStatement, IForeignKeyColumnar<T>
    {
        IForeignKeyReferenceConstraint<T, TR> References<TR>(string table);
    }
}
