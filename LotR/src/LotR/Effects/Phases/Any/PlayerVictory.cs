using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public class PlayerVictory
        : FrameworkEffectBase
    {
        public PlayerVictory(IGame game)
            : base("Player Victory", GetDescription(game), game)
        {
        }

        private static string GetDescription(IGame game)
        {
            return string.Format("The players have won the game with a score of {0}", game.GetPlayerScore());
        }
    }
}
