using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Phases.Combat;

namespace LotR.Effects.CharacterAbilities
{
    public class RangedAbility
        : CharacterAbilityBase, IDuringCharactersAttack
    {
        public RangedAbility(IPlayerCard source)
            : base("Ranged", source)
        {
        }

        public void DuringCharactersAttack(ICharactersAttackStep step)
        {
        }
    }
}
