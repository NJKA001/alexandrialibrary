using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public class PlayerDefeat
        : FrameworkEffectBase
    {
        public PlayerDefeat(IGame game)
            : base("The players have lost the game", game)
        {
        }
    }
}
