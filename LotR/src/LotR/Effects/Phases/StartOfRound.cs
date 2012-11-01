using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Phases
{
    public class StartOfRound
        : FrameworkEffectBase
    {
        public StartOfRound(IGame game)
            : base("Start of Round " + game.CurrentRound, game)
        {
        }
    }
}
