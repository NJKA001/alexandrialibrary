using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Commands;

namespace Gnosis.Core.Batches
{
    public interface IBatch
    {
        void Add(ICommandBuilder builder);
        void Execute();
    }
}
