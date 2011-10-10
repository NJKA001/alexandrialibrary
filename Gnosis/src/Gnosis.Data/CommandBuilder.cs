using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public class CommandBuilder
        : ICommandBuilder
    {
        public CommandBuilder()
        {
            this.CommandType = CommandType.Text;
        }

        public CommandBuilder(string commandText)
            : this()
        {
            sql.Append(commandText);
        }

        private readonly StringBuilder sql = new StringBuilder();
        private readonly IDictionary<string, object> parameters = new Dictionary<string, object>();

        public string CommandText { get { return sql.ToString(); } }
        public int CommandTimeout { get; set; }
        public CommandType CommandType { get; set; }

        public void AddQuotedParameter(string name, object value)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (value == null)
                throw new ArgumentNullException("value");

            parameters.Add(name, string.Format("'{0}'", value));
        }

        public void AddUnquotedParameter(string name, object value)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (value == null)
                throw new ArgumentNullException("value");

            parameters.Add(name, value);
        }

        public void Append(string value)
        {
            sql.Append(value);
        }

        public void AppendFormat(string format, params object[] args)
        {
            sql.AppendFormat(format, args);
        }

        public void AppendFormatLine(string format, params object[] args)
        {
            sql.AppendFormat(format, args);
            sql.AppendLine();
        }

        public void AppendLine()
        {
            sql.AppendLine();
        }

        public void AppendLine(string value)
        {
            sql.AppendLine(value);
        }

        public IDbCommand ToCommand(IDbConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            var command = connection.CreateCommand();
            command.CommandText = sql.ToString();
            command.CommandType = CommandType;

            if (CommandTimeout > 0)
                command.CommandTimeout = CommandTimeout;

            foreach (var pair in parameters)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = pair.Key;
                parameter.Value = pair.Value;
                command.Parameters.Add(parameter);
            }

            return command;
        }
    }
}
