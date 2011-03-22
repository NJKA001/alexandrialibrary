using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface ITrigger : IStatement
    {
        ITriggerType AfterDeleteOn(string table);
        ITriggerType BeforeDeleteOn(string table);
        ITriggerType InsteadOfDeleteOn(string table);

        ITriggerType AfterInsertOn(string table);
        ITriggerType BeforeInsertOn(string table);
        ITriggerType InsteadOfInsertOn(string table);

        ITriggerUpdateOf AfterUpdateOfColumns(string table, params string[] columns);
        ITriggerUpdateOf BeforeUpdateOfColumns(string table, params string[] columns);
        ITriggerUpdateOf InsteadOfUpdateOfColumns(string table, params string[] columns);

        ITriggerType AfterUpdateOn(string table);
        ITriggerType BeforeUpdateOn(string table);
        ITriggerType InsteadOfUpdateOn(string table);
    }

    public interface ITrigger<T> : IStatement
    {
        ITriggerType AfterDeleteOn(string table);
        ITriggerType BeforeDeleteOn(string table);
        ITriggerType InsteadOfDeleteOn(string table);

        ITriggerType AfterInsertOn(string table);
        ITriggerType BeforeInsertOn(string table);
        ITriggerType InsteadOfInsertOn(string table);

        ITriggerUpdateOf AfterUpdateOfColumns(string table, params Expression<Func<T, object>>[] columns);
        ITriggerUpdateOf BeforeUpdateOfColumns(string table, params Expression<Func<T, object>>[] columns);
        ITriggerUpdateOf InsteadOfUpdateOfColumns(string table, params Expression<Func<T, object>>[] columns);

        ITriggerType AfterUpdateOn(string table);
        ITriggerType BeforeUpdateOn(string table);
        ITriggerType InsteadOfUpdateOn(string table);
    }
}
