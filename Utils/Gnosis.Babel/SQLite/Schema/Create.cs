using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    #region Create

    public class Create : Statement, ICreate
    {
        const string KeywordCreate = "create";
        const string KeywordIfNotExists = "if not exists";
        const string KeywordIndex = "index";
        const string KeywordTable = "table";
        const string KeywordTemp = "temp";
        const string KeywordTrigger = "trigger";
        const string KeywordUniqueIndex = "unique index";
        const string KeywordView = "view";

        public IIndex Index(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordIndex);
            return AppendWord<IIndex, Index>(name);
        }

        public IIndex IndexIfNotExists(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordIndex);
            AppendWord(KeywordIfNotExists);
            return AppendWord<IIndex, Index>(name);
        }

        public IIndex UniqueIndex(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordUniqueIndex);
            return AppendWord<IIndex, Index>(name);
        }

        public IIndex UniqueIndexIfNotExists(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordUniqueIndex);
            AppendWord(KeywordIfNotExists);
            return AppendWord<IIndex, Index>(name);
        }

        public ITable Table(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordTable);
            return AppendWord<ITable, Table>(name);
        }

        public ITable TableIfNotExists(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordTable);
            AppendWord(KeywordIfNotExists);
            return AppendWord<ITable, Table>(name);
        }

        public ITable TempTable(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordTemp);
            AppendWord(KeywordTable);
            return AppendWord<ITable, Table>(name);
        }

        public ITable TempTableIfNotExists(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordTemp);
            AppendWord(KeywordTable);
            AppendWord(KeywordIfNotExists);
            return AppendWord<ITable, Table>(name);
        }

        public ITrigger Trigger(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordTrigger);
            return AppendWord<ITrigger, Trigger>(name);
        }

        public ITrigger TriggerIfNotExists(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordTrigger);
            AppendWord(KeywordIfNotExists);
            return AppendWord<ITrigger, Trigger>(name);
        }

        public ITrigger TempTrigger(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordTemp);
            AppendWord(KeywordTrigger);
            return AppendWord<ITrigger, Trigger>(name);
        }

        public ITrigger TempTriggerIfNotExists(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordTemp);
            AppendWord(KeywordTrigger);
            AppendWord(KeywordIfNotExists);
            return AppendWord<ITrigger, Trigger>(name);
        }

        public IView View(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordView);
            AppendWord(KeywordIfNotExists);
            return AppendWord<IView, View>(name);
        }

        public IView ViewIfNotExists(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordView);
            AppendWord(KeywordIfNotExists);
            return AppendWord<IView, View>(name);
        }

        public IView TempView(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordTemp);
            AppendWord(KeywordView);
            return AppendWord<IView, View>(name);
        }

        public IView TempViewIfNotExists(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordTemp);
            AppendWord(KeywordView);
            AppendWord(KeywordIfNotExists);
            return AppendWord<IView, View>(name);
        }
    }

    #endregion

    #region Create<T>

    public class Create<T> : Statement, ICreate<T>
    {
        const string KeywordCreate = "create";
        const string KeywordIfNotExists = "if not exists";
        const string KeywordIndex = "index";
        const string KeywordTable = "table";
        const string KeywordTemp = "temp";
        const string KeywordTrigger = "trigger";
        const string KeywordUniqueIndex = "unique index";
        const string KeywordView = "view";

        public IIndex<T> Index(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordIndex);
            return AppendWord<IIndex<T>, Index<T>>(name);
        }

        public IIndex<T> IndexIfNotExists(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordIndex);
            AppendWord(KeywordIfNotExists);
            return AppendWord<IIndex<T>, Index<T>>(name);
        }

        public IIndex<T> UniqueIndex(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordUniqueIndex);
            return AppendWord<IIndex<T>, Index<T>>(name);
        }

        public IIndex<T> UniqueIndexIfNotExists(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordUniqueIndex);
            AppendWord(KeywordIfNotExists);
            return AppendWord<IIndex<T>, Index<T>>(name);
        }

        public ITable<T> Table(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordTable);
            return AppendWord<ITable<T>, Table<T>>(name);
        }

        public ITable<T> TableIfNotExists(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordTable);
            AppendWord(KeywordIfNotExists);
            return AppendWord<ITable<T>, Table<T>>(name);
        }

        public ITable<T> TempTable(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordTemp);
            AppendWord(KeywordTable);
            return AppendWord<ITable<T>, Table<T>>(name);
        }

        public ITable<T> TempTableIfNotExists(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordTemp);
            AppendWord(KeywordTable);
            AppendWord(KeywordIfNotExists);
            return AppendWord<ITable<T>, Table<T>>(name);
        }

        public ITrigger<T> Trigger(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordTrigger);
            return AppendWord<ITrigger<T>, Trigger<T>>(name);
        }

        public ITrigger<T> TriggerIfNotExists(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordTrigger);
            AppendWord(KeywordIfNotExists);
            return AppendWord<ITrigger<T>, Trigger<T>>(name);
        }

        public ITrigger<T> TempTrigger(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordTemp);
            AppendWord(KeywordTrigger);
            return AppendWord<ITrigger<T>, Trigger<T>>(name);
        }

        public ITrigger<T> TempTriggerIfNotExists(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordTemp);
            AppendWord(KeywordTrigger);
            AppendWord(KeywordIfNotExists);
            return AppendWord<ITrigger<T>, Trigger<T>>(name);
        }

        public IView<T> View(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordView);
            AppendWord(KeywordIfNotExists);
            return AppendWord<IView<T>, View<T>>(name);
        }

        public IView<T> ViewIfNotExists(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordView);
            AppendWord(KeywordIfNotExists);
            return AppendWord<IView<T>, View<T>>(name);
        }

        public IView<T> TempView(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordTemp);
            AppendWord(KeywordView);
            return AppendWord<IView<T>, View<T>>(name);
        }

        public IView<T> TempViewIfNotExists(string name)
        {
            AppendClause(KeywordCreate);
            AppendWord(KeywordTemp);
            AppendWord(KeywordView);
            AppendWord(KeywordIfNotExists);
            return AppendWord<IView<T>, View<T>>(name);
        }
    }

    #endregion
}
