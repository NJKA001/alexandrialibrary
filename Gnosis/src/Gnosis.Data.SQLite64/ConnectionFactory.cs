using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Gnosis.Data.SQLite64
{
    public static class ConnectionFactory
    {
        public static IDbConnection Create(string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException("connectionString");

            return new SQLiteConnection(connectionString);
        }
    }
}
