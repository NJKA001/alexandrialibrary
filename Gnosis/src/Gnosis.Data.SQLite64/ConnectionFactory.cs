using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Data.SQLite64
{
    public static class ConnectionFactory
    {
        public static IDbConnection Create(string connectionString)
        {
            return new System.Data.SQLite.SQLiteConnection(connectionString);
        }
    }
}
