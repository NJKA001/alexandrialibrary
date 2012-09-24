using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Phases.Any;

namespace LotR.Core
{
    public interface IDamageableCard
        : ICard
    {
        void DetermineHitPoints(IDetermineHitPointsStep step);
    }
}
