using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects.Phases;

namespace LotR.Effects.Modifiers
{
    public class AttackModifier
        : ModifierBase, IAttackModifier
    {
        public AttackModifier(IPhase startPhase, ISource source, ICard target, TimeScope duration, int value)
            : base(GetDefaultDescription("Attack", value), startPhase, source, target, duration, value)
        {
        }
    }
}
