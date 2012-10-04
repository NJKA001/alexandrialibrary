using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects.Modifiers
{
    public class AttackModifier
        : ModifierBase, IAttackModifier
    {
        public AttackModifier(IPhase startPhase, ICard source, ICard target, TimeScope duration, int value)
            : base(GetDefaultDescription("Attack", value), startPhase, source, target, duration, value)
        {
        }
    }
}
