using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Data.SQLite
{
    public abstract class SQLiteRepositoryBase
    {
        protected IDbConnection GetConnection(string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException("connectionString");

#if x64
            return Gnosis.Data.SQLite64.ConnectionFactory.Create(connectionString);
#else
            return Gnosis.Data.SQLite32.ConnectionFactory.Create(connectionString);
#endif
        }
    }
}
