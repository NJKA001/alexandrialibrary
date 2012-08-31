using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IPhase
    {
        string Name { get; }

        bool IsCompleted { get; }

        void Start();
        void Complete();
    }
}
