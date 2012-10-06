using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Phases.Any
{
    interface IDuringDamageHealed
    {
        void DuringDamageHealedSetup(IHealCharacterDamageStep step);
        void DuringDamageHealedResolve(IHealCharacterDamageStep step);
    }
}
