using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IDrop
    {
        IStatement Index(string name);
        IStatement IndexIfExists(string name);
        IStatement Table(string name);
        IStatement TableIfExists(string name);
        IStatement Trigger(string name);
        IStatement TriggerIfExists(string name);
        IStatement View(string name);
        IStatement ViewIfExists(string name);
    }
}
