using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Data.Commands
{
    public class CommandBuilder
        : IComplexCommandBuilder
    {
        public CommandBuilder()
            : this(string.Empty, null)
        {
        }

        public CommandBuilder(string name, Type type)
        {
            this.name = name;
            this.type = type;
        }

        private readonly string name;
        private readonly Type type;
        private readonly IList<IComplexCommandBuilder> children = new List<IComplexCommandBuilder>();
        private readonly IDictionary<string, object> parameters = new Dictionary<string, object>();
        private readonly IList<IStatement> statements = new List<IStatement>();
        
        private static void AddParameter(IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }

        #region ICommandBuilder Members

        public string Name
        {
            get { return name; }
        }

        public Type Type
        {
            get { return type; }
        }

        public void AddChild(IComplexCommandBuilder child)
        {
            children.Add(child);
        }

        public void AddParameter(IParameter parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            parameters.Add(parameter.Name, parameter.Value);
        }

        public void AddStatement(IStatement statement)
        {
            if (statement == null)
                throw new ArgumentNullException("statement");

            statements.Add(statement);
        }

        public IDbCommand GetCommand(IDbConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = ToString();
            
            foreach (var parameter in parameters)
                AddParameter(command, parameter.Key, parameter.Value);

            return command;
        }

        public IEnumerable<IComplexCommandBuilder> Children
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
    }
}
