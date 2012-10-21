using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects.Phases;
using LotR.States;

namespace LotR.Effects.Modifiers
{
    public class AttackModifier
        : ModifierBase, IAttackModifier
    {
        public AttackModifier(PhaseCode startPhase, ISource source, ICardInPlay target, TimeScope duration, int value)
            : base(GetDefaultDescription("Attack", value), startPhase, source, target, duration, value)
        {
        }
    }
}
