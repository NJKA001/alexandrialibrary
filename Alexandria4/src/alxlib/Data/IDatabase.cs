using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Data
{
    public interface IDatabase
    {
        string Name { get; }

        void Initialize();
        IDbConnection GetConnection();
    }
}
