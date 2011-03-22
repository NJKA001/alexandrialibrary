using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Babel.SQLite.Persist.Deleting;
using Gnosis.Babel.SQLite.Persist.Inserting;
using Gnosis.Babel.SQLite.Persist.Updating;
using Gnosis.Babel.SQLite.Query;
using Gnosis.Babel.SQLite.Schema;

namespace Gnosis.Babel.SQLite
{
    public class SQLiteStatementFactory : ISQLiteStatementFactory
    {
        public ICreate<T> Create<T>()
        {
            return new Create<T>();
        }

        public IDrop Drop()
        {
            return new Drop();
        }

        public ISelect Select()
        {
            return new Select();
        }

        public IDelete<T> Delete<T>()
            where T : IModel
        {
            return new Delete<T>();
        }

        public IInsert<T> Insert<T>()
            where T : IModel
        {
            return new Insert<T>();
        }

        public IUpdate<T> Update<T>()
            where T : IModel
        {
            return new Update<T>();
        }
    }
}
