using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface ICostlyCard
        : IPlayerCard
    {
        ICost GetResourceCost(IPhaseStep step);
    }
}
