using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Persist.Inserting
{
    public interface IInsertable
    {
        IInsertColumn Into(string table);
    }

    public interface IInsertable<T>
        where T : IModel
    {
        IInsertColumn<T> Into(string table);
    }
}
