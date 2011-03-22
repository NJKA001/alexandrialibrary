using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Schema
{
    public class ColumnConstraint : ColumnConstrained, IColumnConstraint
    {
        private const string KeywordCheck = "check";
        private const string KeywordPrimaryKey = "primary key";
        private const string KeywordUniqueKey = "unique";
        private const string KeywordForeignKey = "foreign key";

        public IColumnName Column(string name)
        {
            return AppendParentheticalListItem<IColumnName, ColumnName>(name);
        }

        public IColumnType Column(string name, string type)
        {
            AppendParentheticalListItem(name);
            return AppendWord<IColumnType, ColumnType>(type);
        }

        public ITableConstraint CheckTable(string expression)
        {
            AppendWord(KeywordCheck);
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

    public class ColumnConstraint<T> : ColumnConstrained<T>, IColumnConstraint<T>
    {
        private const string KeywordCheck = "check";
        private const string KeywordPrimaryKey = "primary key";
        private const string KeywordUniqueKey = "unique";
        private const string KeywordForeignKey = "foreign key";

        public IColumnName<T> Column(Expression<Func<T, object>> expression)
        {
            return AppendParentheticalListItem<IColumnName<T>, ColumnName<T>>(expression.ToName());
        }

        public IColumnType<T> Column(Expression<Func<T, object>> expression, string type)
        {
            AppendParentheticalListItem(expression.ToName());
            return AppendWord<IColumnType<T>, ColumnType<T>>(type);
        }

        public ITableConstraint<T> CheckTable(string expression)
        {
            AppendWord(KeywordCheck);
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
