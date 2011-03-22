using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public class ColumnName : Statement, IColumnName
    {
        const string KeywordBlob = "BLOB";
        const string KeywordInteger = "INTEGER";
        const string KeywordReal = "REAL";
        const string KeywordText = "TEXT";

        public IColumnType Blob
        {
            get { return AppendWord<IColumnType, ColumnType>(KeywordBlob); }
        }

        public IColumnType Integer
        {
            get { return AppendWord<IColumnType, ColumnType>(KeywordInteger); }
        }

        public IColumnType Real
        {
            get { return AppendWord<IColumnType, ColumnType>(KeywordReal); }
        }

        public IColumnType Text
        {
            get { return AppendWord<IColumnType, ColumnType>(KeywordText); }
        }
    }

    public class ColumnName<T> : Statement, IColumnName<T>
    {
        const string KeywordBlob = "BLOB";
        const string KeywordInteger = "INTEGER";
        const string KeywordReal = "REAL";
        const string KeywordText = "TEXT";

        public IColumnType<T> Blob
        {
            get { return AppendWord<IColumnType<T>, ColumnType<T>>(KeywordBlob); }
        }

        public IColumnType<T> Integer
        {
            get { return AppendWord<IColumnType<T>, ColumnType<T>>(KeywordInteger); }
        }

        public IColumnType<T> Real
        {
            get { return AppendWord<IColumnType<T>, ColumnType<T>>(KeywordReal); }
        }

        public IColumnType<T> Text
        {
            get { return AppendWord<IColumnType<T>, ColumnType<T>>(KeywordText); }
        }
    }
}
