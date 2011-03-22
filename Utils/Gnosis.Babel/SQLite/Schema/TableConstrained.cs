namespace Gnosis.Babel.SQLite.Schema
{
    public class TableConstrained : Statement, ITableConstrained
    {
        private const string KeywordCheck = "check";
        private const string KeywordForeignKey = "foreign key";
        private const string KeywordPrimaryKey = "primary key";
        private const string KeywordUniqueKey = "unique";

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

    public class TableConstrained<T> : Statement, ITableConstrained<T>
    {
        private const string KeywordCheck = "check";
        private const string KeywordForeignKey = "foreign key";
        private const string KeywordPrimaryKey = "primary key";
        private const string KeywordUniqueKey = "unique";

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
