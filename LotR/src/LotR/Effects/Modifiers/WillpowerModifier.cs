using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects.Modifiers
{
    public class WillpowerModifier
        : ModifierBase
    {
        public WillpowerModifier(IPhase startPhase, ICard source, ICard target, TimeScope duration, int value)
            : base(GetDefaultDescription("Willpower", value), startPhase, source, target, duration, value)
        {
        }
    }
}
