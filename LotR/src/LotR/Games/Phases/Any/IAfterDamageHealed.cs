using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Payments;

namespace LotR.Games.Phases.Any
{
    public interface IAfterDamageHealed
    {
        void AfterDamageHealed(IHealCharacterDamageStep step);
    }
}
