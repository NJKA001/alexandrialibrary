using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects.Phases;

namespace LotR.Effects.Modifiers
{
    public class WillpowerModifier
        : ModifierBase
    {
        public WillpowerModifier(IPhase startPhase, ISource source, ICard target, TimeScope duration, int value)
            : base(GetDefaultDescription("Willpower", value), startPhase, source, target, duration, value)
        {
        }
    }
}
