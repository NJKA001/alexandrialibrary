using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface ITableConstrained
    {
        ITableConstraint CheckTable(string expression);
        IKeyConstraint PrimaryKey { get; }
        IKeyConstraint UniqueKey { get; }
        IForeignKeyConstraint ForeignKey { get; }
    }

    public interface ITableConstrained<T>
    {
        ITableConstraint<T> CheckTable(string expression);
        IKeyConstraint<T> PrimaryKey { get; }
        IKeyConstraint<T> UniqueKey { get; }
        IForeignKeyConstraint<T> ForeignKey { get; }
    }
}
