using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Persist.Inserting
{
    #region Insert

    public class Insert : Statement, IInsert
    {
        public Insert()
        {
            AppendClause(KeywordInsert);
        }

        private const string KeywordInsert = "insert";
        private const string KeywordInto = "into";
        private const string KeywordOrAbort = "or abort";
        private const string KeywordOrFail = "or fail";
        private const string KeywordOrIgnore = "or ignore";
        private const string KeywordOrReplace = "or replace";
        private const string KeywordOrRollback = "or rollback";

        public IInsertColumn Into(string table)
        {
            AppendClause(KeywordInto);
            return AppendWord<IInsertColumn, InsertColumn>(table);
        }

        public IInsertConflictClause OrAbort
        {
            get { return AppendClause<IInsertConflictClause, InsertConflictClause>(KeywordOrAbort); }
        }

        public IInsertConflictClause OrFail
        {
            get { return AppendClause<IInsertConflictClause, InsertConflictClause>(KeywordOrFail); }
        }

        public IInsertConflictClause OrIgnore
        {
            get { return AppendClause<IInsertConflictClause, InsertConflictClause>(KeywordOrIgnore); }
        }

        public IInsertConflictClause OrReplace
        {
            get { return AppendClause<IInsertConflictClause, InsertConflictClause>(KeywordOrReplace); }
        }

        public IInsertConflictClause OrRollback
        {
            get { return AppendClause<IInsertConflictClause, InsertConflictClause>(KeywordOrRollback); }
        }
    }

    #endregion

    #region Insert<T>

    public class Insert<T> : Statement, IInsert<T>
        where T : IModel
    {
        public Insert()
        {
            AppendClause(KeywordInsert);
        }

        private const string KeywordInsert = "insert";
        private const string KeywordInto = "into";
        private const string KeywordOrAbort = "or abort";
        private const string KeywordOrFail = "or fail";
        private const string KeywordOrIgnore = "or ignore";
        private const string KeywordOrReplace = "or replace";
        private const string KeywordOrRollback = "or rollback";

        public IInsertColumn<T> Into(string table)
        {
            AppendClause(KeywordInto);
            return AppendWord<IInsertColumn<T>, InsertColumn<T>>(table);
        }

        public IInsertConflictClause<T> OrAbort
        {
            get { return AppendClause<IInsertConflictClause<T>, InsertConflictClause<T>>(KeywordOrAbort); }
        }

        public IInsertConflictClause<T> OrFail
        {
            get { return AppendClause<IInsertConflictClause<T>, InsertConflictClause<T>>(KeywordOrFail); }
        }

        public IInsertConflictClause<T> OrIgnore
        {
            get { return AppendClause<IInsertConflictClause<T>, InsertConflictClause<T>>(KeywordOrIgnore); }
        }

        public IInsertConflictClause<T> OrReplace
        {
            get { return AppendClause<IInsertConflictClause<T>, InsertConflictClause<T>>(KeywordOrReplace); }
        }

        public IInsertConflictClause<T> OrRollback
        {
            get { return AppendClause<IInsertConflictClause<T>, InsertConflictClause<T>>(KeywordOrRollback); }
        }
    }

    #endregion
}
