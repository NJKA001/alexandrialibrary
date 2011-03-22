using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Schema
{
    public class ForeignKeyColumn : Statement, IForeignKeyColumn
    {
        private const string KeywordReferences = "references";

        public IForeignKeyReferenceConstraint References(string table)
        {
            AppendWord(KeywordReferences);
            return AppendWord<IForeignKeyReferenceConstraint, ForeignKeyReferenceConstraint>(table);
        }

        public IForeignKeyColumn Column(string name)
        {
            return AppendParentheticalSubListItem<IForeignKeyColumn, ForeignKeyColumn>(name);
        }
    }

    public class ForeignKeyColumn<T> : Statement, IForeignKeyColumn<T>
    {
        private const string KeywordReferences = "references";

        public IForeignKeyReferenceConstraint<T, TR> References<TR>(string table)
        {
            AppendWord(KeywordReferences);
            return AppendWord<IForeignKeyReferenceConstraint<T, TR>, ForeignKeyReferenceConstraint<T, TR>>(table);
        }

        public IForeignKeyColumn<T> Column(Expression<Func<T, object>> expression)
        {
            return AppendParentheticalSubListItem<IForeignKeyColumn<T>, ForeignKeyColumn<T>>(expression.ToName());
        }
    }
}
