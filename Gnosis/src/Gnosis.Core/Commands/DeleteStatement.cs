using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Commands
{
    public class DeleteStatement : IStatement
    {
        public DeleteStatement(string tableName, string whereClause)
        {
            builder.AppendFormat("delete from {0} where {1};", tableName, whereClause);
        }

        private readonly StringBuilder builder = new StringBuilder();

        public override string ToString()
        {
            return builder.ToString();
        }
    }
}
