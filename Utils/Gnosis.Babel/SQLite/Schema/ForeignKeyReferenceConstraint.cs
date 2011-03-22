using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Schema
{
    public class ForeignKeyReferenceConstraint : Statement, IForeignKeyReferenceConstraint
    {
        public IForeignKeyReference Column(string name)
        {
            return AppendParentheticalSubListItem<IForeignKeyReference, ForeignKeyReference>(name);
        }
    }

    public class ForeignKeyReferenceConstraint<T, TR> : Statement, IForeignKeyReferenceConstraint<T, TR>
    {
        public IForeignKeyReference<T, TR> Column(Expression<Func<TR, object>> expression)
        {
            return
                AppendParentheticalSubListItem<IForeignKeyReference<T, TR>, ForeignKeyReference<T, TR>>(
                    expression.ToName());
        }
    }
}
