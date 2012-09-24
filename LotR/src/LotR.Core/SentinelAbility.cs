using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Effects.CharacterAbilities;
using LotR.Core.Phases.Combat;

namespace LotR.Core
{
    public class SentinelAbility
        : CharacterAbilityBase, IDuringCharactersDefend
    {
        public SentinelAbility(IPlayerCard source)
            : base("Sentinel", source)
        {
        }
    }
}
