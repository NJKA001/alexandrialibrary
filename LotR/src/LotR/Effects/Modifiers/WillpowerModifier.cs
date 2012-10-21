using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects.Phases;
using LotR.States;

namespace LotR.Effects.Modifiers
{
    public class WillpowerModifier
        : ModifierBase
    {
        public WillpowerModifier(PhaseCode startPhase, ISource source, ICardInPlay target, TimeScope duration, int value)
            : base(GetDefaultDescription("Willpower", value), startPhase, source, target, duration, value)
        {
        }
    }
}
