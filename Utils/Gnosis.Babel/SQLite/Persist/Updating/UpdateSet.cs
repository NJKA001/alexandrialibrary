using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Persist.Updating
{
    public class UpdateSet : Statement, IUpdateSet
    {
        private const string KeywordSet = "set";

        public IUpdateColumn Set
        {
            get { return AppendClause<IUpdateColumn, UpdateColumn>(KeywordSet); }
        }
    }

    public class UpdateSet<T> : Statement, IUpdateSet<T>
        where T : IModel
    {
        private const string KeywordSet = "set";

        public IUpdateColumn<T> Set
        {
            get { return AppendClause<IUpdateColumn<T>, UpdateColumn<T>>(KeywordSet); }
        }
    }
}
