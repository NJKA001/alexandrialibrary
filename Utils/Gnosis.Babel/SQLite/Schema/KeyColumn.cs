using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Schema
{
    public class KeyColumn : Statement, IKeyColumn
    {
        private const string KeywordAsc = "asc";
        private const string KeywordCheck = "check";
        private const string KeywordDesc = "desc";
        private const string KeywordForeignKey = "foreign key";
        private const string KeywordPrimaryKey = "primary key";
        private const string KeywordUniqueKey = "unique";

        public IKeyColumn ColumnAsc(string name)
        {
            AppendParentheticalListItem(name);
            return AppendWord<IKeyColumn, KeyColumn>(KeywordAsc);
        }

        public IKeyColumn ColumnDesc(string name)
        {
            AppendParentheticalListItem(name);
            return AppendParentheticalListItem<IKeyColumn, KeyColumn>(KeywordDesc);
        }

        public ITableConstraint CheckTable(string expression)
        {
            AppendParentheticalListItem(KeywordCheck);
            return AppendParentheticalSubListItem<ITableConstraint, TableConstraint>(expression);
        }

        public IKeyConstraint PrimaryKey
        {
            get { return AppendParentheticalListItem<IKeyConstraint, KeyConstraint>(KeywordPrimaryKey); }
        }

        public IKeyConstraint UniqueKey
        {
            get { return AppendParentheticalListItem<IKeyConstraint, KeyConstraint>(KeywordUniqueKey); }
        }

        public IForeignKeyConstraint ForeignKey
        {
            get { return AppendParentheticalListItem<IForeignKeyConstraint, ForeignKeyConstraint>(KeywordForeignKey); }
        }
    }

    public class KeyColumn<T> : Statement, IKeyColumn<T>
    {
        private const string KeywordAsc = "asc";
        private const string KeywordCheck = "check";
        private const string KeywordDesc = "desc";
        private const string KeywordForeignKey = "foreign key";
        private const string KeywordPrimaryKey = "primary key";
        private const string KeywordUniqueKey = "unique";

        public IKeyColumn<T> ColumnAsc(Expression<Func<T, object>> expression)
        {
            AppendParentheticalListItem(expression.ToName());
            return AppendWord<IKeyColumn<T>, KeyColumn<T>>(KeywordAsc);
        }

        public IKeyColumn<T> ColumnDesc(Expression<Func<T, object>> expression)
        {
            AppendParentheticalListItem(expression.ToName());
            return AppendWord<IKeyColumn<T>, KeyColumn<T>>(KeywordDesc);
        }

        public ITableConstraint<T> CheckTable(string expression)
        {
            AppendParentheticalListItem(KeywordCheck);
            return AppendParentheticalSubListItem<ITableConstraint<T>, TableConstraint<T>>(expression);
        }

        public IKeyConstraint<T> PrimaryKey
        {
            get { return AppendParentheticalListItem<IKeyConstraint<T>, KeyConstraint<T>>(KeywordPrimaryKey); }
        }

        public IKeyConstraint<T> UniqueKey
        {
            get { return AppendParentheticalListItem<IKeyConstraint<T>, KeyConstraint<T>>(KeywordUniqueKey); }
        }

        public IForeignKeyConstraint<T> ForeignKey
        {
            get { return AppendParentheticalListItem<IForeignKeyConstraint<T>, ForeignKeyConstraint<T>>(KeywordForeignKey); }
        }
    }
}
