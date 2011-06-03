using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Commands
{
    public class CommandBuilder
        : ICommandBuilder
    {
        public CommandBuilder()
            : this(string.Empty)
        {
        }

        public CommandBuilder(string name)
        {
            this.name = name;
        }

        private readonly string name;
        private readonly IList<ICommandBuilder> children = new List<ICommandBuilder>();
        private readonly IDictionary<string, object> parameters = new Dictionary<string, object>();
        private readonly IList<IStatement> statements = new List<IStatement>();
        
        private static void AddParameter(IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }

        #region ICommandBuilber Members

        public string Name
        {
            get { return name; }
        }

        public void AddChild(ICommandBuilder child)
        {
            children.Add(child);
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

        public IEnumerable<ICommandBuilder> Children
        {
            get { return children; }
        }

        #endregion

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
