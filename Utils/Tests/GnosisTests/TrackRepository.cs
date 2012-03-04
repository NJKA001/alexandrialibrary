using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace GnosisTests
{
    class TrackRepository
    {
        private IDbConnection GetConnection()
        {
            return new SQLiteConnection("Data Source=Track.db");
        }
    }
}
