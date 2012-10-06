using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Phases.Combat;

namespace LotR
{
    public class SentinelAbility
        : CharacterAbilityBase, IDuringCharactersDefend
    {
        public SentinelAbility(IPlayerCard source)
            : base("Sentinel", source)
        {
        }

        public void DuringCharactersDefend(ICharactersDefendStep step)
        {
        }
    }
}
