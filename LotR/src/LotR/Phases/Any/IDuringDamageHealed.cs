using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Phases.Any
{
    interface IDuringDamageHealed
    {
        void DuringDamageHealedSetup(IHealDamageStep step);
        void DuringDamageHealedResolve(IHealDamageStep step);
    }
}
