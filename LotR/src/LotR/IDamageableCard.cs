using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Phases.Any;

namespace LotR
{
    public interface IDamageableCard
        : ICard
    {
        void DetermineHitPoints(IDetermineHitPointsStep step);
    }
}
