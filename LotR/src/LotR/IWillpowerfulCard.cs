using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Phases.Any;

namespace LotR
{
    public interface IWillpowerfulCard
        : ICard
    {
        void DetermineWillpower(IDetermineWillpowerStep step);
    }
}
