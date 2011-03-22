using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IColumnName : IStatement
    {
        IColumnType Blob { get; }
        IColumnType Integer { get; }
        IColumnType Real { get; }
        IColumnType Text { get; }
    }

    public interface IColumnName<T> : IStatement
    {
        IColumnType<T> Blob { get; }
        IColumnType<T> Integer { get; }
        IColumnType<T> Real { get; }
        IColumnType<T> Text { get; }
    }
}
