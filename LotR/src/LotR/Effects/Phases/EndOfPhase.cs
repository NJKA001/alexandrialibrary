using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Phases
{
    public class EndOfPhase
        : FrameworkEffectBase
    {
        public EndOfPhase(IGame game)
            : base("End of " + game.CurrentPhase.Name + " Phase", game)
        {
        }
    }
}
