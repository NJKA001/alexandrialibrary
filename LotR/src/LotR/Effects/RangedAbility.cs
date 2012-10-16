using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Effects.Phases.Combat;
using LotR.States.Phases.Combat;

namespace LotR.Effects
{
    public class RangedAbility
        : CharacterAbilityBase, IDuringCharactersAttack
    {
        public RangedAbility(IPlayerCard source)
            : base("Ranged", source)
        {
        }

        public void DuringCharactersAttack(ICharactersAttack state)
        {
        }
    }
}
