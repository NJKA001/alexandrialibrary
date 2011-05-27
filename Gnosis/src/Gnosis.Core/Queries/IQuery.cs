using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Commands;

namespace Gnosis.Core.Queries
{
    public interface IQuery<T>
        where T : IEntity
    {
        void Add(ICommandBuilder builder);
        IEnumerable<T> Execute();
    }
}
