using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Effects.Modifiers
{
    public class AttackModifier
        : ModifierBase, IAttackModifier
    {
        public AttackModifier(IPhase startPhase, ICard source, Duration duration, int value)
            : base(GetDefaultDescription("Attack", value), startPhase, source, duration, value)
        {
        }
    }
}
