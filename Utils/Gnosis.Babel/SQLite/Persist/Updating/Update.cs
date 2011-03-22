using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Persist.Updating
{
    #region Update

    public class Update : Statement, IUpdate
    {
        public Update()
        {
            AppendClause(KeywordUpdate);
        }

        private const string KeywordUpdate = "update";
        private const string KeywordOrAbort = "or abort";
        private const string KeywordOrFail = "or fail";
        private const string KeywordOrIgnore = "or ignore";
        private const string KeywordOrReplace = "or replace";
        private const string KeywordOrRollback = "or rollback";

        public IUpdateSet Table(string table)
        {
            return AppendClause<IUpdateSet, UpdateSet>(table);
        }

        public IUpdateConflictClause OrAbort
        {
            get { return AppendWord<IUpdateConflictClause, UpdateConflictClause>(KeywordOrAbort); }
        }

        public IUpdateConflictClause OrFail
        {
            get { return AppendWord<IUpdateConflictClause, UpdateConflictClause>(KeywordOrFail); }
        }

        public IUpdateConflictClause OrIgnore
        {
            get { return AppendWord<IUpdateConflictClause, UpdateConflictClause>(KeywordOrIgnore); }
        }

        public IUpdateConflictClause OrReplace
        {
            get { return AppendWord<IUpdateConflictClause, UpdateConflictClause>(KeywordOrReplace); }
        }

        public IUpdateConflictClause OrRollback
        {
            get { return AppendWord<IUpdateConflictClause, UpdateConflictClause>(KeywordOrRollback); }
        }
    }

    #endregion

    #region Update<T>

    public class Update<T> : Statement, IUpdate<T>
        where T : IModel
    {
        public Update()
        {
            AppendClause(KeywordUpdate);
        }

        private const string KeywordUpdate = "update";
        private const string KeywordOrAbort = "or abort";
        private const string KeywordOrFail = "or fail";
        private const string KeywordOrIgnore = "or ignore";
        private const string KeywordOrReplace = "or replace";
        private const string KeywordOrRollback = "or rollback";

        public IUpdateSet<T> Table(string table)
        {
            return AppendClause<IUpdateSet<T>, UpdateSet<T>>(table);
        }

        public IUpdateConflictClause<T> OrAbort
        {
            get { return AppendWord<IUpdateConflictClause<T>, UpdateConflictClause<T>>(KeywordOrAbort); }
        }

        public IUpdateConflictClause<T> OrFail
        {
            get { return AppendWord<IUpdateConflictClause<T>, UpdateConflictClause<T>>(KeywordOrFail); }
        }

        public IUpdateConflictClause<T> OrIgnore
        {
            get { return AppendWord<IUpdateConflictClause<T>, UpdateConflictClause<T>>(KeywordOrIgnore); }
        }

        public IUpdateConflictClause<T> OrReplace
        {
            get { return AppendWord<IUpdateConflictClause<T>, UpdateConflictClause<T>>(KeywordOrReplace); }
        }

        public IUpdateConflictClause<T> OrRollback
        {
            get { return AppendWord<IUpdateConflictClause<T>, UpdateConflictClause<T>>(KeywordOrRollback); }
        }
    }

    #endregion
}
