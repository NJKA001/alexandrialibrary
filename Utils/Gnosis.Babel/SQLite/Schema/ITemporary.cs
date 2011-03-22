using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface ITemporary
    {
        IIndex Index(string name);
        IIndex IndexIfNotExists(string name);
        ITable Table(string name);
        ITable TableIfNotExists(string name);
        ITrigger Trigger(string name);
        ITrigger TriggerIfNotExists(string name);
        IView View(string name);
        IView ViewIfNotExists(string name);
    }
}
