using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Schema
{
    public class ForeignKeyReference : Statement, IForeignKeyReference
    {
        private const string KeywordCheck = "check";
        private const string KeywordForeignKey = "foreign key";
        private const string KeywordPrimaryKey = "primary key";
        private const string KeywordUniqueKey = "unique";

        public IForeignKeyReference Column(string name)
        {
            return AppendParentheticalSubListItem<IForeignKeyReference, ForeignKeyReference>(name);
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

    public class ForeignKeyReference<T, TR> : Statement, IForeignKeyReference<T, TR>
    {
        private const string KeywordCheck = "check";
        private const string KeywordForeignKey = "foreign key";
        private const string KeywordPrimaryKey = "primary key";
        private const string KeywordUniqueKey = "unique";

        public IForeignKeyReference<T, TR> Column(Expression<Func<TR, object>> expression)
        {
            return
                AppendParentheticalSubListItem<IForeignKeyReference<T, TR>, ForeignKeyReference<T, TR>>(
                    expression.ToName());
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
