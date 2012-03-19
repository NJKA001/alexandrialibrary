using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Data.Commands
{
    public interface IComplexCommandBuilder
    {
        string Name { get; }
        Type Type { get; }

        void AddChild(IComplexCommandBuilder builder);
        void AddParameter(IParameter parameter);
        void AddStatement(IStatement statement);

        IDbCommand GetCommand(IDbConnection connection);
        IEnumerable<IComplexCommandBuilder> Children { get; }
    }
}
