using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Commands
{
    public abstract class CommandBuilder
    {
        protected CommandBuilder()
        {
        }

        protected CommandBuilder(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                    this.parameters.Add(parameter);
            }
        }

        private readonly IDictionary<string, object> parameters = new Dictionary<string, object>();
        private readonly IList<IStatement> statements = new List<IStatement>();
        
        private static void AddParameter(IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }

        public void AddParameter(string name, object value)
        {
            parameters.Add(name, value);
        }

        public void AddStatement(IStatement statement)
        {
            statements.Add(statement);
        }

        public string GetParameterName()
        {
            return string.Format("@{0}", Guid.NewGuid().ToString().Replace("-", string.Empty));
        }

        public IDbCommand GetCommand(IDbConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = ToString();
            
            foreach (var parameter in parameters)
                AddParameter(command, parameter.Key, parameter.Value);

            return command;
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
