using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface ICreate
    {
        IIndex Index(string name);
        IIndex IndexIfNotExists(string name);
        IIndex UniqueIndex(string name);
        IIndex UniqueIndexIfNotExists(string name);

        ITable Table(string name);
        ITable TableIfNotExists(string name);
        ITable TempTable(string name);
        ITable TempTableIfNotExists(string name);

        IView TempView(string name);
        IView TempViewIfNotExists(string name);

        ITrigger Trigger(string name);
        ITrigger TriggerIfNotExists(string name);
        ITrigger TempTrigger(string name);
        ITrigger TempTriggerIfNotExists(string name);

        IView View(string name);        
        IView ViewIfNotExists(string name);
    }

    public interface ICreate<T>
    {
        IIndex<T> Index(string name);
        IIndex<T> IndexIfNotExists(string name);
        IIndex<T> UniqueIndex(string name);
        IIndex<T> UniqueIndexIfNotExists(string name);

        ITable<T> Table(string name);
        ITable<T> TableIfNotExists(string name);
        ITable<T> TempTable(string name);
        ITable<T> TempTableIfNotExists(string name);

        ITrigger<T> Trigger(string name);
        ITrigger<T> TriggerIfNotExists(string name);
        ITrigger<T> TempTrigger(string name);
        ITrigger<T> TempTriggerIfNotExists(string name);

        IView<T> View(string name);
        IView<T> ViewIfNotExists(string name);
        IView<T> TempView(string name);
        IView<T> TempViewIfNotExists(string name);
    }
}
