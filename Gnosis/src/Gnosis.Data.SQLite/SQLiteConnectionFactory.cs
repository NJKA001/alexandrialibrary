#if x64
extern alias sqlite64;
#endif

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Data.SQLite
{
    public class SQLiteConnectionFactory
        : IConnectionFactory
    {
        public IDbConnection Create(string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException("connectionString");

#if x64
            return new sqlite64.System.Data.SQLite.SQLiteConnection(connectionString);
            //return Gnosis.Data.SQLite64.ConnectionFactory.Create(connectionString);
#else
            return new System.Data.SQLite.SQLiteConnection(connectionString);
            //return Gnosis.Data.SQLite32.ConnectionFactory.Create(connectionString);
#endif
        }
    }
}
