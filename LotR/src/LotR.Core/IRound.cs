using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IRound
    {
        IPhase CurrentPhase { get; }
        bool IsCompleted { get; }

        void Start();
        void Complete();
    }
}
