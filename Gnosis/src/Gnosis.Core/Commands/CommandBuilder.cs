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

        public Type Type
        {
            get { return type; }
        }

        public void AddChild(ICommandBuilder child)
        {
            children.Add(child);
        }

        public void AddParameter(string name, object value)
        {
            var normalizedValue = value;

            if (value != null)
            {
                if (value is IEntity)
                    normalizedValue = ((IEntity)value).Id.ToString();
                if (value is IValue)
                    normalizedValue = ((IValue)value).Id.ToString();
                if (value.GetType() == typeof(bool))
                    normalizedValue = (bool)value ? 1 : 0;
                if (value.GetType() == typeof(Guid))
                    normalizedValue = ((Guid)value).ToString();
                else if (value.GetType() == typeof(Uri))
                    normalizedValue = ((Uri)value).ToString();
                else if (value.GetType() == typeof(DateTime))
                    normalizedValue = ((DateTime)value).ToString("s");
                else if (value.GetType() == typeof(TimeSpan))
                    normalizedValue = ((TimeSpan)value).Ticks;
                else if (value.GetType().IsEnum)
                    normalizedValue = (int)value;
            }

            parameters.Add(name, normalizedValue);
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
