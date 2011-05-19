using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Commands
{
    public interface IUnitOfWork
    {
        void Add(ICommandBuilder builder);
        void Execute();
    }
}
