using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Query
{
    public interface IJoinCondition : IStatement, IJoinable, IFilterable
    {
        IJoin And<T>(Expression<Func<T, object>> expression);
        IJoin Or<T>(Expression<Func<T, object>> expression);
    }
}
