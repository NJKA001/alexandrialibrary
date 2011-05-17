using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class ForeignKeyInfo
    {
        public ForeignKeyInfo(string tableName)
        {
            this.tableName = tableName;
        }

        private readonly string tableName;

        public string TableName
        {
            get { return tableName; }
        }
    }
}
