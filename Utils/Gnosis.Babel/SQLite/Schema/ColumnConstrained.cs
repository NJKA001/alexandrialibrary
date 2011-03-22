using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public class ColumnConstrained : Statement, IColumnConstrained
    {
        private const string KeywordPrimaryKeyAsc = "primary key asc";
        private const string KeywordPrimaryKeyAutoIncrement = "primary key autoincrement";
        private const string KeywordPrimaryKeyDesc = "primary key desc";
        private const string KeywordNotNull = "not null";
        private const string KeywordUnique = "unique";
        private const string KeywordCheck = "check";
        private const string KeywordDefault = "default";
        private const string KeywordCollateBinary = "collate binary";
        private const string KeywordCollateNoCase = "collate nocase";
        private const string KeywordCollateRightTrim = "collate rtrim";

        public IColumnConstraint PrimaryKeyAsc
        {
            get { return AppendWord<IColumnConstraint, ColumnConstraint>(KeywordPrimaryKeyAsc); }
        }

        public IColumnConstraint PrimaryKeyAutoIncrement
        {
            get { return AppendWord<IColumnConstraint, ColumnConstraint>(KeywordPrimaryKeyAutoIncrement); }
        }

        public IColumnConstraint PrimaryKeyDesc
        {
            get { return AppendWord<IColumnConstraint, ColumnConstraint>(KeywordPrimaryKeyDesc); }
        }

        public IColumnConstraint NotNull
        {
            get { return AppendWord<IColumnConstraint, ColumnConstraint>(KeywordNotNull); }
        }

        public IColumnConstraint Unique
        {
            get { return AppendWord<IColumnConstraint, ColumnConstraint>(KeywordUnique); }
        }

        public IColumnConstraint CheckColumn(string expression)
        {
            AppendWord(KeywordCheck);
            return AppendParentheticalSubListItem<IColumnConstraint, ColumnConstraint>(expression);
        }

        public IColumnConstraint Default(object value)
        {
            AppendWord(KeywordDefault);
            return AppendWord<IColumnConstraint, ColumnConstraint>(value.AsDefaultString());
        }

        public IColumnConstraint CollateBinary
        {
            get { return AppendWord<IColumnConstraint, ColumnConstraint>(KeywordCollateBinary); }
        }

        public IColumnConstraint CollateCaseInsensitve
        {
            get { return AppendWord<IColumnConstraint, ColumnConstraint>(KeywordCollateNoCase); }
        }

        public IColumnConstraint CollateRightTrim
        {
            get { return AppendWord<IColumnConstraint, ColumnConstraint>(KeywordCollateRightTrim); }
        }

        public ITableConstraint TableConstraints
        {
            get { return Transform<ITableConstraint, TableConstraint>(); }
        }
    }

    public class ColumnConstrained<T> : Statement, IColumnConstrained<T>
    {
        private const string KeywordPrimaryKeyAsc = "primary key asc";
        private const string KeywordPrimaryKeyAutoIncrement = "primary key autoincrement";
        private const string KeywordPrimaryKeyDesc = "primary key desc";
        private const string KeywordNotNull = "not null";
        private const string KeywordUnique = "unique";
        private const string KeywordCheck = "check";
        private const string KeywordDefault = "default";
        private const string KeywordCollateBinary = "collate binary";
        private const string KeywordCollateNoCase = "collate nocase";
        private const string KeywordCollateRightTrim = "collate rtrim";

        public IColumnConstraint<T> PrimaryKeyAsc
        {
            get { return AppendWord<IColumnConstraint<T>, ColumnConstraint<T>>(KeywordPrimaryKeyAsc); }
        }

        public IColumnConstraint<T> PrimaryKeyAutoIncrement
        {
            get { return AppendWord<IColumnConstraint<T>, ColumnConstraint<T>>(KeywordPrimaryKeyAutoIncrement); }
        }

        public IColumnConstraint<T> PrimaryKeyDesc
        {
            get { return AppendWord<IColumnConstraint<T>, ColumnConstraint<T>>(KeywordPrimaryKeyDesc); }
        }

        public IColumnConstraint<T> NotNull
        {
            get { return AppendWord<IColumnConstraint<T>, ColumnConstraint<T>>(KeywordNotNull); }
        }

        public IColumnConstraint<T> Unique
        {
            get { return AppendWord<IColumnConstraint<T>, ColumnConstraint<T>>(KeywordUnique); }
        }

        public IColumnConstraint<T> CheckColumn(string expression)
        {
            AppendWord(KeywordCheck);
            return AppendParentheticalSubListItem<IColumnConstraint<T>, ColumnConstraint<T>>(expression);
        }

        public IColumnConstraint<T> Default(object value)
        {
            AppendWord(KeywordDefault);
            return AppendWord<IColumnConstraint<T>, ColumnConstraint<T>>(value.AsDefaultString());
        }

        public IColumnConstraint<T> CollateBinary
        {
            get { return AppendWord<IColumnConstraint<T>, ColumnConstraint<T>>(KeywordCollateBinary); }
        }

        public IColumnConstraint<T> CollateCaseInsensitve
        {
            get { return AppendWord<IColumnConstraint<T>, ColumnConstraint<T>>(KeywordCollateNoCase); }
        }

        public IColumnConstraint<T> CollateRightTrim
        {
            get { return AppendWord<IColumnConstraint<T>, ColumnConstraint<T>>(KeywordCollateRightTrim); }
        }

        public ITableConstraint<T> TableConstraints
        {
            get { return Transform<ITableConstraint<T>, TableConstraint<T>>(); }
        }
    }
}
