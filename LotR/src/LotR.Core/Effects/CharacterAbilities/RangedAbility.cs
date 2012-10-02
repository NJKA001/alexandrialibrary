using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Phases.Combat;

namespace LotR.Core.Effects.CharacterAbilities
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

        public override void Resolve(IPhaseStep step, IPayment payment)
        {
        }
    }
}
