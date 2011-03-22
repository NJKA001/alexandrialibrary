using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IColumnConstrained
    {
        IColumnConstraint PrimaryKeyAsc { get; }
        IColumnConstraint PrimaryKeyAutoIncrement { get; }
        IColumnConstraint PrimaryKeyDesc { get; }
        IColumnConstraint NotNull { get; }
        IColumnConstraint Unique { get; }
        IColumnConstraint CheckColumn(string expression);
        IColumnConstraint Default(object value);
        IColumnConstraint CollateBinary { get; }
        IColumnConstraint CollateCaseInsensitve { get; }
        IColumnConstraint CollateRightTrim { get; }
        ITableConstraint TableConstraints { get; }
    }

    public interface IColumnConstrained<T>
    {
        IColumnConstraint<T> PrimaryKeyAsc { get; }
        IColumnConstraint<T> PrimaryKeyAutoIncrement { get; }
        IColumnConstraint<T> PrimaryKeyDesc { get; }
        IColumnConstraint<T> NotNull { get; }
        IColumnConstraint<T> Unique { get; }
        IColumnConstraint<T> CheckColumn(string expression);
        IColumnConstraint<T> Default(object value);
        IColumnConstraint<T> CollateBinary { get; }
        IColumnConstraint<T> CollateCaseInsensitve { get; }
        IColumnConstraint<T> CollateRightTrim { get; }
        ITableConstraint<T> TableConstraints { get; }
    }
}
