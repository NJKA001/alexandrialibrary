using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using FirebirdSql.Data.FirebirdClient;

namespace Gnosis.Data.Firebird
{
    public class FirebirdConnectionFactory
        : IConnectionFactory
    {
        public IDbConnection Create(string connectionString)
        {
            return new FbConnection(connectionString);
        }

        public void CreateDatabase(string connectionString)
        {
            FbConnection.CreateDatabase(connectionString, false);
        }
    }
}
