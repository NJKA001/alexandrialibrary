using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Modifiers
{
    public class ThreatModifier
        : ModifierBase, IThreatModifier
    {
        public ThreatModifier(PhaseCode startPhase, ISource source, IThreateningInPlay target, TimeScope duration, int value)
            : base(GetDefaultDescription("Threat", value), startPhase, source, target, duration, value)
        {
        }
    }
}
