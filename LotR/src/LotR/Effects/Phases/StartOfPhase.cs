using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Phases
{
    public class StartOfPhase
        : FrameworkEffectBase
    {
        public StartOfPhase(IGame game)
            : base("Start of Phase", "The start of the " + game.CurrentPhase.Name + " phase", game)
        {
        }
    }
}
