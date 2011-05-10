using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
    public class CommandBuilder
    {
        private readonly IList<IStatementBuilder> statements = new List<IStatementBuilder>();

        public void Add(IStatementBuilder statement)
        {
            statements.Add(statement);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            foreach (var statement in statements)
                builder.AppendLine(statement.ToString());

            return builder.ToString();
        }

        public string ToUnformattedString()
        {
            return ToString().Replace(Environment.NewLine, " ");
        }
    }
}
