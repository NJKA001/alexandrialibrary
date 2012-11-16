using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.Effects.Phases.Combat;
using LotR.States.Phases.Combat;

namespace LotR.Effects
{
    public class SentinelAbility
        : CharacterAbilityBase, IDuringCharactersDefend
    {
        public SentinelAbility(IPlayerCard source)
            : base("Sentinel", "This character can block enemies that are attacking other players", source)
        {
        }

        public void DuringCharactersDefend(IEnemyAttack state)
        {
        }

        public override string ToString()
        {
            return "Sentinel";
        }
    }
}
