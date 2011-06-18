using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Commands
{
    public interface ICommandBuilder
    {
        string Name { get; }
        Type Type { get; }

        void AddChild(ICommandBuilder builder);
        void AddParameter(IParameter parameter);
        void AddStatement(IStatement statement);

        IDbCommand GetCommand(IDbConnection connection);
        IEnumerable<ICommandBuilder> Children { get; }
    }
}
