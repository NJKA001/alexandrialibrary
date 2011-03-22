using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public class Trigger : Statement, ITrigger
    {
        private const string KeywordAfterDelete = "after delete";
        private const string KeywordBeforeDelete = "before delete";
        private const string KeywordInsteadOfDelete = "instead of delete";
        private const string KeywordAfterInsert = "after insert";
        private const string KeywordBeforeInsert = "before insert";
        private const string KeywordInsteadOfInsert = "instead of insert";
        private const string KeywordAfterUpdate = "after update";
        private const string KeywordBeforeUpdate = "before update";
        private const string KeywordInsteadOfUpdate = "instead of update";
        private const string KeywordOf = "of"; 
        private const string KeywordOn = "on";

        public ITriggerType AfterDeleteOn(string table)
        {
            throw new NotImplementedException();
        }

        public ITriggerType BeforeDeleteOn(string table)
        {
            throw new NotImplementedException();
        }

        public ITriggerType InsteadOfDeleteOn(string table)
        {
            throw new NotImplementedException();
        }

        public ITriggerType AfterInsertOn(string table)
        {
            throw new NotImplementedException();
        }

        public ITriggerType BeforeInsertOn(string table)
        {
            throw new NotImplementedException();
        }

        public ITriggerType InsteadOfInsertOn(string table)
        {
            throw new NotImplementedException();
        }

        public ITriggerUpdateOf AfterUpdateOfColumns(string table, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ITriggerUpdateOf BeforeUpdateOfColumns(string table, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ITriggerUpdateOf InsteadOfUpdateOfColumns(string table, params string[] columns)
        {
            throw new NotImplementedException();
        }

        public ITriggerType AfterUpdateOn(string table)
        {
            throw new NotImplementedException();
        }

        public ITriggerType BeforeUpdateOn(string table)
        {
            throw new NotImplementedException();
        }

        public ITriggerType InsteadOfUpdateOn(string table)
        {
            throw new NotImplementedException();
        }
    }

    public class Trigger<T> : Statement, ITrigger<T>
    {
        public ITriggerType AfterDeleteOn(string table)
        {
            throw new NotImplementedException();
        }

        public ITriggerType BeforeDeleteOn(string table)
        {
            throw new NotImplementedException();
        }

        public ITriggerType InsteadOfDeleteOn(string table)
        {
            throw new NotImplementedException();
        }

        public ITriggerType AfterInsertOn(string table)
        {
            throw new NotImplementedException();
        }

        public ITriggerType BeforeInsertOn(string table)
        {
            throw new NotImplementedException();
        }

        public ITriggerType InsteadOfInsertOn(string table)
        {
            throw new NotImplementedException();
        }

        public ITriggerUpdateOf AfterUpdateOfColumns(string table, params Expression<Func<T, object>>[] columns)
        {
            throw new NotImplementedException();
        }

        public ITriggerUpdateOf BeforeUpdateOfColumns(string table, params Expression<Func<T, object>>[] columns)
        {
            throw new NotImplementedException();
        }

        public ITriggerUpdateOf InsteadOfUpdateOfColumns(string tabe, params Expression<Func<T, object>>[] columns)
        {
            throw new NotImplementedException();
        }

        public ITriggerType AfterUpdateOn(string table)
        {
            throw new NotImplementedException();
        }

        public ITriggerType BeforeUpdateOn(string table)
        {
            throw new NotImplementedException();
        }

        public ITriggerType InsteadOfUpdateOn(string table)
        {
            throw new NotImplementedException();
        }
    }
}
