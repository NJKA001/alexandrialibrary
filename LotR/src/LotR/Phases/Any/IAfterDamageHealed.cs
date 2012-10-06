using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Phases.Any
{
    public interface IAfterDamageHealed
    {
        void AfterDamageHealedSetup(IHealCharacterDamageStep step);
        void AfterDamageHealedResolve(IHealCharacterDamageStep step, IPayment payment);
    }
}
