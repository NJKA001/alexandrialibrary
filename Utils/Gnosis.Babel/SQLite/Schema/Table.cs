using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Gnosis.Babel.SQLite.Query;

namespace Gnosis.Babel.SQLite.Schema
{
    public class Table : Statement, ITable
    {
        const string KeywordAs = "as";

        public ISelect AsSelect
        {
            get { return AppendClause<ISelect, Select>(KeywordAs); }
        }

        public IColumnName Column(string name)
        {
            return AppendParentheticalListItem<IColumnName, ColumnName>(name);
        }

        public IColumnType Column(string name, string type)
        {
            AppendParentheticalListItem(name);
            return AppendWord<IColumnType, ColumnType>(type);
        }
    }

    public class Table<T> : Statement, ITable<T>
    {
        const string KeywordAs = "as";

        public ISelect AsSelect
        {
            get { return AppendClause<ISelect, Select>(KeywordAs); }
        }

        public IColumnName<T> Column(Expression<Func<T, object>> expression)
        {
            return AppendParentheticalListItem<IColumnName<T>, ColumnName<T>>(expression.ToName());
        }

        public IColumnType<T> Column(Expression<Func<T, object>> expression, string type)
        {
            AppendParentheticalListItem(expression.ToName());
            return AppendWord<IColumnType<T>, ColumnType<T>>(type);
        }
    }

}
