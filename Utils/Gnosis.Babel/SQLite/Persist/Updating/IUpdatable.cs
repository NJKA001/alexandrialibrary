using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Persist.Updating
{
    public interface IUpdatable
    {
        IUpdateSet Table(string table);
    }

    public interface IUpdatable<T>
        where T : IModel
    {
        IUpdateSet<T> Table(string table);
    }
}
