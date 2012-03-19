using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Data.Commands;

namespace Gnosis.Data.Batches
{
    public interface IBatch
    {
        void Add(IComplexCommandBuilder builder);
        void Execute();
    }
}
