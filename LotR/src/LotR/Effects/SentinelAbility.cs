using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Effects.Phases.Combat;

namespace LotR.Effects
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
