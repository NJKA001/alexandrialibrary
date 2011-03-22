using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Persist.Deleting
{
    #region Delete

    public class Delete : Statement, IDelete
    {
        private const string KeywordFrom = "from";

        public IDeleteFrom From(string table)
        {
            AppendClause(KeywordFrom);
            return AppendWord<IDeleteFrom, DeleteFrom>(table);
        }
    }

    #endregion

    #region Delete<T>

    public class Delete<T> : Statement, IDelete<T>
        where T : IModel
    {
        private const string KeywordFrom = "from";

        public IDeleteFrom<T> From(string table)
        {
            AppendClause(KeywordFrom);
            return AppendWord<IDeleteFrom<T>, DeleteFrom<T>>(table);
        }
    }

    #endregion
}
