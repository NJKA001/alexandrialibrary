using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface IConnectionFactory
    {
        IDbConnection Create(string connectionString);
    }
}
